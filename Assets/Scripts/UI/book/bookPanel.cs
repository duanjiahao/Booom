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
    //文字信息
    private Text totalDay;
    private Text currentDay;
    private Text totalGuest;
    //病人的信息Unit prefab
    public GameObject npcUnitPrefab;
    private GameObject npcItem;
    private List<GameObject> prefabList = new List<GameObject>();
    //翻页功能
    //当前的npc索引
    private int currentNPCIndex = 0;
    private int end;
    //一次翻页的物体数量
    private int itemSize = 3;
    public Toggle toggleUp;  // 向上翻页的Toggle
    public Toggle toggleDown; // 向下翻页的Toggle
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    void FindInfo()
    {
        BtExit = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtExit");
        totalDay = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "totalDay");
        currentDay = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "currentDay");
        totalGuest = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "totalGuestNumber");
    }
    // Start is called before the first frame update
    void Start()
    {
        //npcUnitPrefab = Resources.Load<GameObject>("Prefab/Book/jiekeRecordUnitL");
        BtExit.onClick.AddListener(() =>
        {
            PagingRoot.SetActive(false);
        });
    }

    public void RefreshBookData()
    {
        // 刷新病历数据
        Debug.Log("current day:" + DataManager.Instance.Day.ToString());
        currentDay.text = "第" + DataManager.Instance.Day.ToString() + "日";
        totalDay.text = "共" + DataManager.Instance.Day.ToString() + "日";
        totalGuest.text = NPCDataManager.Instance.GetNPCs().Count.ToString();

        // 每次刷新时重置到第一页
        currentNPCIndex = 0; 
        DisplayNPCItems(currentNPCIndex);
        UpdateToggleStates();
    }

    public void OnToggleUp()
    {
        // 向前翻页逻辑
        if (currentNPCIndex - itemSize >= 0)
        {
            currentNPCIndex -= itemSize;
            DisplayNPCItems(currentNPCIndex);
            Debug.Log("Moved back: Current index: " + currentNPCIndex);
        }
        UpdateToggleStates();
    }

    public void OnToggleDown()
    {
        // 向后翻页逻辑
        if (currentNPCIndex + itemSize < NPCDataManager.Instance.GetNPCs().Count)
        {
            currentNPCIndex += itemSize;
            DisplayNPCItems(currentNPCIndex);
            Debug.Log("Moved down: Current index: " + currentNPCIndex);
        }
        UpdateToggleStates();
    }

    void UpdateToggleStates()
    {
        toggleUp.interactable = currentNPCIndex > 0;
        toggleDown.interactable = currentNPCIndex + itemSize < NPCDataManager.Instance.GetNPCs().Count;
    }
    private void ClearItemList()
    {
        //清理列表
        if (prefabList.Count != 0)
        {
            foreach (var item in prefabList)
            {
                Destroy(item);
            }
            prefabList.Clear();
        }

    }
    private void DisplayNPCItems(int startIndex)
    {
        ClearItemList();
        List<NPCItem> npcList = NPCDataManager.Instance.GetNPCs();
        int end = Mathf.Min(startIndex + itemSize, npcList.Count);

        // 根据起始和结束索引显示item
        for (int i = startIndex; i < end; i++)
        {
            NPCItem data = npcList[i];
            GameObject npcItem = Instantiate(npcUnitPrefab, transform.position, Quaternion.identity, transform.Find("NPCList"));
            npcItem.GetComponent<NPCInfo>().SetNPCInfo(data);
            prefabList.Add(npcItem);
        }
    }

}
