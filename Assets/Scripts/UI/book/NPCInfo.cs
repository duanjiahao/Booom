using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
public class NPCInfo : MonoBehaviour
{
    private GameObject PagingRoot;
    //NPC info
    private Text NPCName;
    private Text NPCPrestige;
    private Text FinalPrestige;
    private Text NeedText;
    private Text AvoidText;
    private Text ResultText;
    private Text SideEffect;
    private Image SideEffectImg;
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    void FindInfo()
    {
        
        NPCName = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NPCName");
        NPCPrestige = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NPCPrestige");
        NeedText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NeedText");
        ResultText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "ResultText");
        SideEffect = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "SideEffect");
        AvoidText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "AvoidText");
        FinalPrestige = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "FinalPrestige");
        SideEffectImg = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "SideEffectImg");
    }
    public void SetNPCInfo(NPCItem info)
    {
        NPCName.text = info.NpcUnit.Name;
        NPCPrestige.text = info.NpcUnit._npcConfig.prestigeLevel[0].ToString();
        FinalPrestige.text = info.FinalPrestige.ToString();
        NeedText.text = string.Join("\n", NPCDataManager.Instance.GetNeedText(info.NpcUnit));
        AvoidText.text = string.Join("\n", NPCDataManager.Instance.GetAvoidText(info.NpcUnit));
        ResultText.text = info.GivenRecipe.Name;
        switch (info.FinalPrestige)
        {
            //选择对应表情
            case 100001:
                break;
            case 100002:
                break;
            case 100003:
                break;
            case 100004:
                break;
        }
        if (info.FinalEffectsList.Count != 0)
        {
            //选择一个副作用进行显示
            var effect = info.FinalEffectsList[Random.Range(0, info.FinalEffectsList.Count)];
            SideEffect.text = effect.EffectAxisConfig.name;
        }

    }
}
