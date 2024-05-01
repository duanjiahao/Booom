using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindFunc;
using UnityEngine.UI;
//用于控制病历界面的UI
public class bookPanel : MonoBehaviour
{
    private GameObject PagingRoot;
    private Button BtExit;
    private Button BtPrev;
    private Button BtNext;
    //文字信息
    private Text totalDay;
    private Text currentDay;
    private Text totalGuest;
    //病人的信息Unit prefab
    private GameObject npcUnitPrefab;
    private GameObject npcItem;
    private List<GameObject> prefabList = new List<GameObject>();
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    void FindInfo()
    {
        BtExit = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtExit");
        BtPrev = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtPrev");
        BtNext = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtNext");
        totalDay = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "totalDay");
        currentDay = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "currentDay");
        totalGuest = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "totalGuestNumber");
    }
    // Start is called before the first frame update
    void Start()
    {
        npcUnitPrefab = Resources.Load<GameObject>("Prefab/Book/jiekeRecordUnitL");
        BtExit.onClick.AddListener(() =>
        {
            PagingRoot.SetActive(false);
        });
        BtPrev.onClick.AddListener(() =>
        {
            OnPrevBtClicked();
        });
        BtNext.onClick.AddListener(() =>
        {
            OnNextBtClicked();
        });
    }
    public void RefreshBookData()
    {
        //刷新病历数据
        Debug.Log(DataManager.Instance.Day.ToString());
        Debug.Log(NPCDataManager.Instance.GetNPCs().Count.ToString());
        currentDay.text = "第" + DataManager.Instance.Day.ToString() + "日";
        totalDay.text = "共" + DataManager.Instance.Day.ToString() + "日";
        totalGuest.text = NPCDataManager.Instance.GetNPCs().Count.ToString();
        ShowNPCData();
    }
    private void OnPrevBtClicked()
    {

    }
    private void OnNextBtClicked()
    {

    }
    public void ShowNPCData()
    {
        List<NPCItem> npcList = NPCDataManager.Instance.GetNPCs();
        if (prefabList.Count != 0)
        {
            //先清空列表
            foreach(var obj in prefabList)
            {
                Destroy(obj);
            }
        }
        if (npcList != null)
        {

            foreach(var npc in npcList)
            {
                npcItem = Instantiate(
npcUnitPrefab,
transform.position,
Quaternion.identity, transform.Find("NPCList")
);
                prefabList.Add(npcItem);
                //需求列表赋值
                npcItem.GetComponent<NPCInfo>().SetNPCInfo(npc);
            }
        }
    }
}
