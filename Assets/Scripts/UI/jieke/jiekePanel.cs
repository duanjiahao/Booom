using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
public class jiekePanel : MonoBehaviour
{
    #region 变量定义
    private GameObject PagingRoot;
    //Buttons
    private Button BtRingBell;
    private Button BtBook;
    private Button BtGoBackyard;
    //NPC info
    private NPCUnit npc;
    private Text NPCName;
    private Text NPCPrestige;
    private Text NeedText;
    private Text AvoidText;
    private Text avoidDialogue;
    private Text needDialogue;
    private Image NPCImage;
    private bool HaveAnotherNPC = false;
    //跳转到别的界面
    public GameObject BookPanel;
    public GameObject BackyardPanel;
    //给出的药方
    private RecipeItem thisRecipe;
    //结算副作用列表
    public List<EffectInfoData> SideEffects = new List<EffectInfoData>();
    #endregion
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    void FindInfo()
    {
        BtRingBell = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtRingBell");
        BtBook = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtBook");
        BtGoBackyard = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtGoBackyard");
        NPCName = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NPCName");
        NPCPrestige = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NPCPrestige");
        NeedText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NeedText");
        avoidDialogue = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "avoidDialogue");
        needDialogue = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "needDialogue");
        AvoidText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "AvoidText");
        NPCImage = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "NPCImage");
    }
    // Start is called before the first frame update
    void Start()
    {
        BookPanel.SetActive(false);
        BtRingBell.onClick.AddListener(() =>
        {
            OnBellRing();
        });
        BtBook.onClick.AddListener(() =>
        {
            
            OpenBook();
        });
        BtGoBackyard.onClick.AddListener(() =>
        {
            GoBackyard();
        });
    }
    private void OnBellRing()
    {
        //摇铃判断
        if (DataManager.Instance.CurrentTime.Equals(5) || DataManager.Instance.CurrentTime.Equals(6))
        {
            //判断是否在迎客时间
            //TODO 加上UI
            Debug.Log("时间太晚了，已经没有客人了");
        }
        else
        {
            //有客状态
            HaveAnotherNPC = true;
            GenerateNewNPC();
        }
    }
    private void GenerateNewNPC()
    {
        //召唤一位新的NPC
        npc = NPCDataManager.Instance.GetNewNPC();
        NPCDataManager.Instance.InitNpcInfo(npc);
        NPCName.text = npc.Name;
        NPCPrestige.text = npc._npcConfig.prestigeLevel[0].ToString();
        needDialogue.text = npc._npcNeedDialogConfig.desc;
        avoidDialogue.text = npc._npcAvoidDialogConfig.desc;
        //正面需求
        string result1 = string.Join("\n", NPCDataManager.Instance.GetNeedText(npc)); // 使用空字符串作为分隔符
        NeedText.text = result1;
        //负面禁忌
        string result2 = string.Join("\n", NPCDataManager.Instance.GetAvoidText(npc)); // 使用空字符串作为分隔符
        AvoidText.text = result2;
        Debug.Log(npc.ImgPath);
    }
    private void OpenBook()
    {
        //打开病历
        BookPanel.SetActive(true);
        //重新读取病历数据
        var info = BookPanel.GetComponent<bookPanel>();
        if (info != null)
        {
            info.RefreshBookData();
        }
    }
    private void GoBackyard()
    {
        //TODO Test!!!
        //thisRecipe = RecipeDataManager.Instance.GetRecipeItemByID(1);
        //Debug.Log(npc.Name);
        //SetResultData();
    }
    //private void SetResultData()
    //{
    //    int prestige = npc._npcConfig.prestigeLevel[0];
    //    //将结算数据传回npc datamanager
    //    switch (CheckResult(thisRecipe,npc))
    //    {
    //        case 1:
    //            prestige = (int)(prestige * 1.5);
    //            break;
    //        case 2:
    //            break;
    //        case 3:
    //            prestige = (int)(prestige * -0.5);
    //            break;
    //        case 4:
    //            prestige *= -1;
    //            break;
    //    }
    //    NPCDataManager.Instance.SetNpcInfo(npc, thisRecipe, prestige, SideEffects);
    //}
    //private int CheckResult(RecipeItem recipe, NPCUnit npc)
    //{
    //    //结算逻辑
    //    int needResult = 0;
    //    int avoidResult = 0;
    //    int sideResult = 0;

    //    if (npc._needEffectIds.Count != 0)
    //    {
    //        //需求结算
    //        foreach(int id in npc._needEffectIds)
    //        {
    //            foreach(var effectItem in recipe.EffectList)
    //            {
    //                if(effectItem.EffectInfo.id == id)
    //                {
    //                    //正面效果计数+1
    //                    needResult += 1;
    //                }
    //            }
    //        }
    //    }
    //    if (npc._avoidEffectIds.Count != 0)
    //    {
    //        //禁忌结算
    //        foreach (int id in npc._avoidEffectIds)
    //        {
    //            foreach (var effectItem in recipe.EffectList)
    //            {
    //                if (!effectItem.EffectInfo.isPositive)
    //                {
    //                    //是负面效果
    //                    if (effectItem.EffectInfo.id == id)
    //                    {
    //                        //禁忌效果计数+1
    //                        avoidResult += 1;
    //                    }
    //                    else
    //                    {
    //                        //副作用效果计数+1
    //                        sideResult += 1;
    //                        if (effectItem.IsVisible)
    //                        {
    //                            //效果可见时才添加到最终显示的副作用列表里
    //                            SideEffects.Add(effectItem);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    if (avoidResult > 0)
    //    {
    //        //结局D：触犯禁忌
    //        return 4;
    //    }
    //    else
    //    {
    //        if (needResult == npc._needEffectIds.Count)
    //        {
    //            if (sideResult==0)
    //            {
    //                //A:完全治愈
    //                return 1;
    //            }
    //            else
    //            {
    //                //B:治愈但有其他不良反应
    //                return 2;
    //            }
    //        }
    //        else
    //        {
    //            //C:未能治愈
    //            return 3;
    //        }
    //    }
    //}
}
