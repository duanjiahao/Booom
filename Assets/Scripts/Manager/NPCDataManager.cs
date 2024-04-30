using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCItem
{
    //npc动态数据定义
    public NPCUnit NpcUnit;
    public RecipeItem GivenRecipe;
    public int FinalPrestige;
    public int FinalReward;
    public List<EffectItem> FinalEffectsList;

}
//NPC数据管理单例
public class NPCDataManager : Singleton<NPCDataManager>
{
    private List<NPCItem> _npcs;
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
    public void SetNpcInfo(NPCUnit unit, RecipeItem recipe, int prestige, int reward, List<EffectItem> finalEffects)
    {
        //完成一个任务后结算，将npc数据计入
        NPCItem tempItem = new NPCItem();
        tempItem.NpcUnit = unit;
        tempItem.GivenRecipe = recipe;
        tempItem.FinalPrestige = prestige;
        tempItem.FinalReward = reward;
        tempItem.FinalEffectsList = finalEffects;
        _npcs.Add(tempItem);
    }

   
}
