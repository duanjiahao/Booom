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
    //public int FinalReward;
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
    public void InitNpcInfo(NPCUnit unit)
    {
        NPCItem tempItem = new NPCItem();
        tempItem.NpcUnit = unit;
        nowNPC = tempItem;
        
    }
   
    public void CompleteNpcInfo(int prestige, List<EffectInfoData> finalEffects)
    {
        //结算npc并添加到列表中
        if (nowNPC == null)
        {
            Debug.LogError("No current NPC to update.");
            return;
        }
        nowNPC.FinalPrestige = prestige;
        nowNPC.FinalEffectsList = finalEffects;
        //添加到npc列表中
        _npcs.Add(nowNPC);
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
            needStrings.Add(effect.name);
        }
        
        return needStrings;
        
    }
    public List<string> GetAvoidText(NPCUnit npc)
    {
        //获取负面禁忌列表
        List<string> avoidStrings = new List<string>();
        foreach (var item in npc._avoidEffectIds)
        {
            EffectAxisConfig effect = ConfigManager.Instance.GetConfig<EffectAxisConfig>(item);
            avoidStrings.Add(effect.name);
        }
        return avoidStrings;
    }

    public void CheckResult()
    {
        //结算流程：计算正面、禁忌、副作用数量并进行结局判断，将声望值和副作用列表赋值给这个npc
        int needResult = CalculatePositiveEffects();
        int avoidResult = CalculateNegativeEffects(out List<EffectInfoData> sideEffects);

        int endResult = DetermineOutcome(needResult, avoidResult, sideEffects.Count);
        int prestige = CalculatePrestige(endResult);

        CompleteNpcInfo(prestige, sideEffects);
    }

    private int CalculatePositiveEffects()
    {
        //获取药方达成的正面效果数量
        int count = 0;
        var effects = nowNPC.GivenRecipe.GetEffectList();
        foreach (int id in nowNPC.NpcUnit._needEffectIds)
        {
            if (effects.Any(e => e.EffectAxisConfig.id == id))
                count++;
        }
        return count;
    }

    private int CalculateNegativeEffects(out List<EffectInfoData> sideEffects)
    {
        //获取药方达成的负面效果数量
        int count = 0;
        sideEffects = new List<EffectInfoData>();
        var effects = nowNPC.GivenRecipe.GetEffectList();
        //遍历禁忌列表进行对应
        foreach (int id in nowNPC.NpcUnit._avoidEffectIds)
        {
            foreach (var effect in effects)
            {
                //若是负面效果
                if (!effect.EffectAxisConfig.isPositive)
                {
                    //若是禁忌效果则计数
                    if (effect.EffectAxisConfig.id == id)
                        count++;
                    else if (effect.IsVisible)
                    //若是副作用则添加到副作用列表里
                        sideEffects.Add(effect);
                }
            }
        }
        return count;
    }

    private int DetermineOutcome(int needResult, int avoidResult, int sideResult)
    {
        //判断哪种结局
        if (avoidResult > 0)
            return 4; // D: 触犯禁忌
        if (needResult == nowNPC.NpcUnit._needEffectIds.Count)
            return (sideResult == 0) ? 1 : 2; // A: 完全治愈, B: 治愈但有副作用
        return 3; // C: 未能治愈
    }

    private int CalculatePrestige(int outcome)
    {
        //计算声望变化
        //TODO 改成正确的声望
        int basePrestige = nowNPC.NpcUnit._npcConfig.prestigeLevel[0];
        switch (outcome)
        {
            case 1: return (int)(basePrestige * 1.5);
            case 2: return basePrestige;
            case 3: return (int)(basePrestige * -0.5);
            case 4: return -basePrestige;
            default: return basePrestige;
        }
    }
    

}
