using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindFunc;
using UnityEngine.UI;
public class bookPanel : MonoBehaviour
{
    private GameObject PagingRoot;
    private Button BtExit;
    private Button BtPrev;
    private Button BtNext;
    private Text totalDay;
    private Text currentDay;
    private Text totalGuest;
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
    public void InitBookData()
    {
        //刷新病历数据
        Debug.Log(DataManager.Instance.Day.ToString());
        Debug.Log(NPCDataManager.Instance.GetNPCs().Count.ToString());
        currentDay.text = "第" + DataManager.Instance.Day.ToString() + "日";
        totalDay.text = "共" + DataManager.Instance.Day.ToString() + "日";
        totalGuest.text = NPCDataManager.Instance.GetNPCs().Count.ToString();
    }
    private void OnPrevBtClicked()
    {

    }
    private void OnNextBtClicked()
    {

    }
}
