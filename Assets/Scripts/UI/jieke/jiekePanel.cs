using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
public class jiekePanel : MonoBehaviour
{
    public GameObject PagingRoot;
    //Buttons
    public Button BtRingBell;
    public Button BtBook;
    public Button BtGoBackyard;
    //NPC info
    public Text NPCName;
    public Text NPCPrestige;
    public Text NeedText;
    public Text AvoidText;
    public Image NPCImage;
    public bool HaveAnotherNPC = false;
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
        AvoidText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "AvoidText");
        NPCImage = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "NPCImage");
    }
    // Start is called before the first frame update
    void Start()
    {

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
        NPCUnit npc = NPCDataManager.Instance.GetNewNPC();
        NPCName.text = npc.Name;
        Debug.Log(npc.ImgPath);
    }
    private void OpenBook()
    {

    }
    private void GoBackyard()
    {

    }
}
