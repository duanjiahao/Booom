using System.Collections;
using System.Collections.Generic;
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
    public List<EffectItem> FinalEffectsList;

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
        //_npcs.Add(npc);
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
        _npcs.Add(tempItem);
        nowNPC = tempItem;
    }
    public void SetNpcInfo(NPCUnit unit, RecipeItem recipe, int prestige, List<EffectItem> finalEffects)
    {
        //完成一个任务后结算，将npc数据计入
        NPCItem tempItem = new NPCItem();
        tempItem.NpcUnit = unit;
        tempItem.GivenRecipe = recipe;
        tempItem.FinalPrestige = prestige;
        tempItem.FinalEffectsList = finalEffects;
        //_npcs.Add(tempItem);
        nowNPC = null;
    }
    public void TreatNPC(RecipeItem recipe)
    {
        //给npc药
        if (_npcs != null)
        {
            //TODO nowNPC赋值有问题
            Debug.Log("now npc is :" + nowNPC.NpcUnit.Name);
            nowNPC.GivenRecipe = recipe;
            //foreach (var npc in _npcs)
            //{
            //    if (npc.NpcUnit.Name.Equals(nowNPC.NpcUnit.Name))
            //    {
            //        npc.GivenRecipe = recipe;
            //    }
            //}
        }
        
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
    private void SetResultData()
    {
        
    }
    public void CheckResult()
    {
        //结算逻辑
        int needResult = 0;
        int avoidResult = 0;
        int sideResult = 0;
        //结算副作用列表
        List<EffectItem> SideEffects = new List<EffectItem>();
        if (nowNPC.NpcUnit._needEffectIds.Count != 0)
        {
            //需求结算
            foreach (int id in nowNPC.NpcUnit._needEffectIds)
            {
                foreach (var effectItem in nowNPC.GivenRecipe.EffectList)
                {
                    if (effectItem.EffectInfo.id == id)
                    {
                        //正面效果计数+1
                        needResult += 1;
                    }
                }
            }
        }
        if (nowNPC.NpcUnit._avoidEffectIds.Count != 0)
        {
            //禁忌结算
            foreach (int id in nowNPC.NpcUnit._avoidEffectIds)
            {
                foreach (var effectItem in nowNPC.GivenRecipe.EffectList)
                {
                    if (!effectItem.EffectInfo.isPositive)
                    {
                        //是负面效果
                        if (effectItem.EffectInfo.id == id)
                        {
                            //禁忌效果计数+1
                            avoidResult += 1;
                        }
                        else
                        {
                            //副作用效果计数+1
                            sideResult += 1;
                            if (effectItem.IsVisible)
                            {
                                //效果可见时才添加到最终显示的副作用列表里
                                SideEffects.Add(effectItem);
                            }
                        }
                    }
                }
            }
        }
        int endResult;
        if (avoidResult > 0)
        {
            //结局D：触犯禁忌
            endResult = 4;
        }
        else
        {
            if (needResult == nowNPC.NpcUnit._needEffectIds.Count)
            {
                if (sideResult == 0)
                {
                    //A:完全治愈
                    endResult = 1;
                }
                else
                {
                    //B:治愈但有其他不良反应
                    endResult = 2;
                }
            }
            else
            {
                //C:未能治愈
                endResult = 3;
            }
        }
        //TODO:改成正确声望
        int prestige = nowNPC.NpcUnit._npcConfig.prestigeLevel[0];
        //将结算数据传回npc datamanager
        switch (endResult)
        {
            case 1:
                prestige = (int)(prestige * 1.5);
                break;
            case 2:
                break;
            case 3:
                prestige = (int)(prestige * -0.5);
                break;
            case 4:
                prestige *= -1;
                break;
        }
        SetNpcInfo(nowNPC.NpcUnit, nowNPC.GivenRecipe, prestige, SideEffects);
    }

}
