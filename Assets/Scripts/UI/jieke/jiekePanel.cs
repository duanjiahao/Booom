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
        else if(NPCDataManager.Instance.IsBusy())
        {
            //有客状态
            Debug.Log("现在还有其他客人，再等等吧");
        }
        else
        {
            //叫下一位客人
            GenerateNewNPC();
        }
    }
    private void GenerateNewNPC()
    {
        //召唤一位新的NPC
        npc = NPCDataManager.Instance.GetNewNPC();
        NPCDataManager.Instance.InitNpcInfo(npc);
        NPCName.text = npc.Name;
        NPCPrestige.text = npc.NPCPrestige.ToString();
        needDialogue.text = npc._npcNeedDialogConfig.desc;
        avoidDialogue.text = npc._npcAvoidDialogConfig.desc;
        //正面需求
        string result1 = string.Join("\n", NPCDataManager.Instance.GetNeedText(npc)); // 使用空字符串作为分隔符
        NeedText.text = result1;
        //负面禁忌
        string result2 = string.Join("\n", NPCDataManager.Instance.GetAvoidText(npc)); // 使用空字符串作为分隔符
        AvoidText.text = result2;
        //人物立绘
        NPCImage.sprite = Resources.Load<Sprite>(npc.ImgPath);
        Debug.Log(npc.ImgPath);
    }
    private void OpenBook()
    {
        //打开病历
        UIManager.Instance.OpenBingLiWindow();
        
    }
    private void GoBackyard()
    {
        UIManager.Instance.OpenBackyardWindow();
    }
    
}
