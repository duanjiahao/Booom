using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class NPCItem
{
    //npc动态数据定义
    public NPCUnit NpcUnit;
    //玩家给出的药方
    public RecipeItem GivenRecipe;
    //最终声望结算
    public int FinalPrestige;
    //最终结局
    public int FinalResponse;
    //副作用列表
    public List<EffectInfoData> FinalEffectsList;

}
//NPC数据管理单例
public class NPCDataManager : Singleton<NPCDataManager>
{
    private List<NPCItem> _npcs;
    private NPCItem nowNPC;
    public NPCDataManager()
    {
        _npcs = new List<NPCItem>();
    }

    public NPCUnit GetNewNPC()
    {
        //获得一个新的npc
        var npcGenerator = NPCGeneratorFactory.GetNPCGenerator();
        NPCUnit npc = npcGenerator.GenerateARandomNPC();
        return npc;
    }

    public List<NPCItem> GetNPCs()
    {
        //获取所有npc
        return _npcs;
    }
    public void ResetNPCData()
    {
        //清空npc数据
        _npcs.Clear();
        nowNPC = null;
    }
    public NPCItem GetNowNPC()
    {
        if (nowNPC != null)
        {
            return nowNPC;
        }
        else
        {
            return null;
        }
    }
    public void InitNpcInfo(NPCUnit unit)
    {
        NPCItem tempItem = new NPCItem();
        tempItem.NpcUnit = unit;
        tempItem.FinalResponse = 0;
        nowNPC = tempItem;
        
    }
    
    public void CompleteNpcInfo(int prestige, List<EffectInfoData> finalEffects, int finalResponseType)
    {
        //结算npc并添加到列表中
        if (nowNPC == null)
        {
            Debug.LogError("No current NPC to update.");
            return;
        }
        nowNPC.FinalPrestige = prestige;
        nowNPC.FinalEffectsList = finalEffects;
        nowNPC.FinalResponse = finalResponseType;
        //添加到npc列表中
        _npcs.Insert(0,nowNPC);
        //ClearCurrentNPC();
        //DataManager.Instance.MoveToNextTime();
        DataManager.Instance.ChangePrestige(prestige);
    }
    public bool IsNPCTreat()
    {
        if (nowNPC.FinalResponse!=0)
            return true;
        else
            return false;
    }
    public void TreatNPC(RecipeItem recipe)
    {
        //给npc药
        if (nowNPC == null)
        {
            Debug.LogError("No NPC available to treat.");
            return;
        }
        nowNPC.GivenRecipe = recipe;
        Debug.Log("now npc is treated with :" + recipe.Name);
    }
    public List<string> GetNeedText(NPCUnit npc)
    {
        //获取正面需求列表
        List<string> needStrings = new List<string>();
        foreach (var item in npc._needEffectIds)
        {
            EffectAxisConfig effect = ConfigManager.Instance.GetConfig<EffectAxisConfig>(item);
            string text = "";
            switch (effect.attributes)
            {
                case (int)EffectAttributeType.Yang:
                    text = "<color=#FF8730>" + effect.name + "</color>";
                    break;
                case (int)EffectAttributeType.Yin:
                    text = "<color=#BD69FF>" + effect.name + "</color>";
                    break;
                case (int)EffectAttributeType.Han:
                    text = "<color=#2E89FF>" + effect.name + "</color>";
                    break;
                case (int)EffectAttributeType.Re:
                    text = "<color=#FF57A0>" + effect.name + "</color>";
                    break;
            }
            needStrings.Add(text);
        }
        
        return needStrings;
        
    }
    public List<string> GetAvoidText(NPCUnit npc)
    {
        //获取负面禁忌列表
        List<string> avoidStrings = new List<string>();

        foreach (var item in npc._avoidEffectIds)
        {
            string text = "";
            EffectAxisConfig effect = ConfigManager.Instance.GetConfig<EffectAxisConfig>(item);
            switch (effect.attributes)
            {
                case (int)EffectAttributeType.Yang:
                    text = "<color=#FF8730>" + effect.name + "</color>";
                    break;
                case (int)EffectAttributeType.Yin:
                    text = "<color=#BD69FF>" + effect.name + "</color>";
                    break;
                case (int)EffectAttributeType.Han:
                    text = "<color=#2E89FF>" + effect.name + "</color>";
                    break;
                case (int)EffectAttributeType.Re:
                    text = "<color=#FF57A0>" + effect.name + "</color>";
                    break;
            }
            avoidStrings.Add(text);
        }
        return avoidStrings;
    }

    public void CheckResult()
    {
        //结算流程：计算正面、禁忌、副作用数量并进行结局判断，将声望值和副作用列表赋值给这个npc
        int needResult = CalculatePositiveEffects(out List<EffectInfoData> corresBadEffects);
        int avoidResult = CalculateNegativeEffects(corresBadEffects,out int sideEffectNum,out List<EffectInfoData> sideEffects);

        int endResult = DetermineOutcome(needResult, avoidResult, sideEffectNum);
        int prestige = CalculatePrestige(endResult);

        CompleteNpcInfo(prestige, sideEffects, endResult);
    }

    private int CalculatePositiveEffects(out List<EffectInfoData> corresBadEffects)
    {
        //获取药方达成的正面效果数量
        int count = 0;
        var effects = nowNPC.GivenRecipe.GetEffectList();
        corresBadEffects = new List<EffectInfoData>();
        foreach (int id in nowNPC.NpcUnit._needEffectIds)
        {
            var matchedEffects = effects.Where(e => e.EffectAxisConfig.id == id).ToList();

            foreach (var effect in matchedEffects)
            {
                count++;
                EffectAxisConfig goodEffect = effect.EffectAxisConfig;
                EffectAxisConfig badeffect = CommonUtils.GetCorrespondBadEffectConfig(goodEffect);
                bool visible = effect.IsVisible;
                corresBadEffects.Add(new EffectInfoData(badeffect, visible));
            }
        }
        return count;
    }

    private int CalculateNegativeEffects(List<EffectInfoData> corresBadeffect,out int sideEffectNum, out List<EffectInfoData> sideEffects)
    {
        //获取药方达成的负面效果数量
        int count = 0;
        sideEffects = new List<EffectInfoData>();
        sideEffectNum = 0;
        var effects = nowNPC.GivenRecipe.GetEffectList();
        // 遍历禁忌列表进行对应
        HashSet<int> avoidEffectIds = new HashSet<int>(nowNPC.NpcUnit._avoidEffectIds);

        foreach (var effect in effects)
        {
            // 只处理负面效果
            if (!effect.EffectAxisConfig.isPositive)
            {
                // 检查是否为禁忌效果
                if (avoidEffectIds.Contains(effect.EffectAxisConfig.id))
                {
                    count++;
                }
                else if (!corresBadeffect.Contains(effect))
                {
                    sideEffectNum++;
                    if (effect.IsVisible)
                    {
                        sideEffects.Add(effect); // 可见的副作用添加到列表
                    }
                }
            }
        }
        return count;
    }

    private int DetermineOutcome(int needResult, int avoidResult, int sideResultNum)
    {
        //判断哪种结局
        if (avoidResult > 0)
            return 4; // D: 触犯禁忌
        if (needResult == nowNPC.NpcUnit._needEffectIds.Count)
            return (sideResultNum == 0) ? 1 : 2; // A: 完全治愈, B: 治愈但有副作用
        return 3; // C: 未能治愈
    }

    private int CalculatePrestige(int outcome)
    {
        //计算声望变化
        int basePrestige = nowNPC.NpcUnit.NPCPrestige;
        switch (outcome)
        {
            case 1: return (int)(basePrestige * 1.5);
            case 2: return basePrestige;
            case 3: return (int)(basePrestige * -0.5);
            case 4: return -basePrestige;
            default: return basePrestige;
        }
    }

    // 确保nowNPC被正确管理
    public void ClearCurrentNPC()
    {
        nowNPC = null;
    }
    // 检查现在是否有客人
    public bool IsBusy()
    {
        if (nowNPC == null)
            return false;
        else
            return true;
    }
    public int GetFinalResponse()
    {
        return nowNPC.FinalResponse;
    }
}
