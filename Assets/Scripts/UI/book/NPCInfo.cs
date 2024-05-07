using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
using System.Linq;
//病历中的npc数据赋值
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
    private Image HeadImage;
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
        HeadImage = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "HeadImage");
        SideEffectImg = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "SideEffectImg");
    }
    public void SetNPCInfo(NPCItem info)
    {
        NPCName.text = info.NpcUnit.Name;
        NPCPrestige.text = info.NpcUnit.NPCPrestige.ToString();
        FinalPrestige.text = info.FinalPrestige.ToString();
        NeedText.text = string.Join("\n", NPCDataManager.Instance.GetNeedText(info.NpcUnit));
        AvoidText.text = string.Join("\n", NPCDataManager.Instance.GetAvoidText(info.NpcUnit));
        ResultText.text = info.GivenRecipe.Name;
        HeadImage.sprite = Resources.Load<Sprite>(info.NpcUnit.HeadImgPath);
        switch (info.FinalResponse)
        {
            //选择对应表情
            case 1:
                SideEffectImg.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_治愈");
                break;
            case 2:
                SideEffectImg.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_部分治愈");
                break;
            case 3:
                SideEffectImg.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_未治愈");
                break;
            case 4:
                SideEffectImg.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_触犯禁忌");
                break;
        }
        if (info.FinalEffectsList.Count != 0 && info.FinalResponse!=4)
        {
            //显示副作用
            var effect = info.FinalShowEffect;
            //选择一个副作用进行显示
            switch (effect.EffectAxisConfig.attributes)
            {
                case (int)EffectAttributeType.Yang:
                    SideEffect.text = "<color=#FF8730>" + effect.EffectAxisConfig.name + "</color>";
                    break; 
                case (int)EffectAttributeType.Yin:
                    SideEffect.text = "<color=#BD69FF>" + effect.EffectAxisConfig.name + "</color>";
                    break;
                case (int)EffectAttributeType.Han:
                    SideEffect.text = "<color=#2E89FF>" + effect.EffectAxisConfig.name + "</color>";
                    break;
                case (int)EffectAttributeType.Re:
                    SideEffect.text = "<color=#FF57A0>" + effect.EffectAxisConfig.name + "</color>";
                    break;
            }
            
        }
        else
        {
            SideEffect.text = "";
        }

    }
}
