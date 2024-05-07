using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;

public class jiekePanel : MonoBehaviour
{
    #region 变量定义
    private GameObject PagingRoot;
    private Image bgImg;
    //npc显示和隐藏
    private Image closeNpcDetail;
    public GameObject openNpcDetail;
    public GameObject endDialogue;
    public GameObject enquiryDialogue;
    private Image answerFace;
    private Text answerText;
    //Buttons
    private Button BtRingBell;
    private Button BtBook;
    private Button BtGoBackyard;
    private Button BtNewDay;
    //NPC info
    private NPCUnit npc;
    private Text NPCName;
    private Text NPCPrestige;
    private Text NeedText;
    private Text AvoidText;
    private Text avoidDialogue;
    private Text needDialogue;
    private Image NPCImage;
    //跳转到别的界面
    public GameObject BookPanel;
    //结算副作用列表
    public List<EffectInfoData> SideEffects = new List<EffectInfoData>();
    
    public IntroductionHelper introductionHelper;
    private float displayDuration = 3f;
    public GameObject AnimationPrefab;
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
        BtNewDay = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtNewDay");
        NPCName = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NPCName");
        NPCPrestige = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NPCPrestige");
        NeedText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "NeedText");
        avoidDialogue = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "avoidDialogue");
        needDialogue = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "needDialogue");
        AvoidText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "AvoidText");
        NPCImage = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "NPCImage");
        bgImg = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "bg");
        closeNpcDetail = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "closeNpcDetail");
        answerFace = UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "Face");
        answerText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "AnswerText");
    }
    private void OnEnable()
    {
        RefreshPanelBg();
        if (NPCDataManager.Instance.GetNowNPC() != null)
        {
            DisplayNowNPC();
        }
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
        BtNewDay.onClick.AddListener(() =>
        {
            //DataManager.Instance.MoveToNextTime();
            //TODO animation
            //PlayTransition();
            //RefreshPanelBg();
        });
        if (PlayerPrefs.GetInt("Introduction_JikeWindow", 0) == 0)
        {
            introductionHelper.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Introduction_JikeWindow", 1);
        }
    }
    public void PlayEndingDialogue()
    {
        enquiryDialogue.SetActive(false);
        //结算npc回应
        int answerType = NPCDataManager.Instance.GetFinalResponse();
        var answerList = ConfigManager.Instance.GetConfigListWithFilter<NPCResponseConfig>((con) =>
        {
            return con.responseType == answerType;
        }) as List<NPCResponseConfig>;
        
        switch (answerType)
        {
            case 1:
                //完全治愈
                answerFace.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_治愈");
                break;
            case 2:
                //治愈但有副作用
                answerFace.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_部分治愈");
                break;
            case 3:
                //未治愈
                answerFace.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_未治愈");
                break;
            case 4:
                //触碰禁忌
                answerFace.sprite = Resources.Load<Sprite>("Arts/Icon/face/icon_触碰禁忌");
                break;
        }
        answerText.text = answerList[Random.Range(0, answerList.Count - 1)].desc;
        endDialogue.SetActive(true);
        StartCoroutine(EndDaySequence());
        //// 在3秒后隐藏窗口
        //Invoke("HideEndDialogue", displayDuration);

        //if (DataManager.Instance.CurrentTime == TimeOfDay.EndOfDay)
        //{
        //    BtNewDay.gameObject.SetActive(true);
        //    bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/打烊");
        //}

    }
    IEnumerator EndDaySequence()
    {
        // 等待指定的持续时间
        yield return new WaitForSeconds(displayDuration);

        // 隐藏对话框
        HideEndDialogue();

        // 显示打烊界面
        if (DataManager.Instance.CurrentTime == TimeOfDay.EndOfDay)
        {
            BtNewDay.gameObject.SetActive(true);
            bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/打烊");
        }
    }
    private void HideEndDialogue()
    {
        RefreshPanelBg();
        //endDialogue.SetActive(false); // 隐藏窗口
        NPCDataManager.Instance.ClearCurrentNPC();
    }
    public void RefreshPanelBg()
    {
        //刷新接客界面
        endDialogue.SetActive(false);
        BtNewDay.gameObject.SetActive(false);
        closeNpcDetail.gameObject.SetActive(true);
        openNpcDetail.SetActive(false);
        switch (DataManager.Instance.CurrentTime)
        {
            case TimeOfDay.Morning_1:
            case TimeOfDay.Morning_2:
            
                bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/清晨");
                break;
            case TimeOfDay.Afternoon_1:
            case TimeOfDay.Afternoon_2:
            
                bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/黄昏");
                break;
            case TimeOfDay.Evening_1:
            case TimeOfDay.Evening_2:
            case TimeOfDay.EndOfDay:
                bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/黑夜");
                break;
            //case TimeOfDay.Count:
            //    BtNewDay.gameObject.SetActive(true);
            //    bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/打烊");
            //    break;
        }
    }
    private void OnBellRing()
    {
        //摇铃判断
        

        if (NPCDataManager.Instance.IsBusy())
        {
            //有客状态
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"还有患者正在等待中~");
        }
        else if (DataManager.Instance.CurrentTime.Equals(TimeOfDay.EndOfDay))
        {
            //打烊时间
            bgImg.sprite = Resources.Load<Sprite>("Arts/场景资源/打烊");
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"时间太晚了，已经没有客人了");
        }
        else
        {
            BtRingBell.GetComponent<Animator>().Play("BellRing", 0);
            AudioManager.Instance.PlayAudio("OpenDoor", false);
            DataManager.Instance.MoveToNextTime();

            StartCoroutine(NextGuest());

            //RefreshPanelBg();
            //叫下一位客人
            //GenerateNewNPC();
            //openNpcDetail.SetActive(true);
        }
    }

    IEnumerator NextGuest()
    {
        // 等待指定时间
        yield return new WaitForSeconds(1f);

        
        RefreshPanelBg();
        GenerateNewNPC();
    }

    private void GenerateNewNPC()
    {
        //召唤一位新的NPC
        npc = NPCDataManager.Instance.GetNewNPC();
        NPCDataManager.Instance.InitNpcInfo(npc);
        DisplayNowNPC();
    }
    private void DisplayNowNPC()
    {
        NPCItem npc = NPCDataManager.Instance.GetNowNPC();
        NPCName.text = npc.NpcUnit.Name;
        NPCPrestige.text = npc.NpcUnit.NPCPrestige.ToString();
        needDialogue.text = npc.NpcUnit._npcNeedDialogConfig.desc;
        avoidDialogue.text = npc.NpcUnit._npcAvoidDialogConfig.desc;
        //正面需求
        string result1 = string.Join("\n", NPCDataManager.Instance.GetNeedText(npc.NpcUnit)); // 使用空字符串作为分隔符
        NeedText.text = result1;
        //负面禁忌
        string result2 = string.Join("\n", NPCDataManager.Instance.GetAvoidText(npc.NpcUnit)); // 使用空字符串作为分隔符
        AvoidText.text = result2;
        //人物立绘
        NPCImage.sprite = Resources.Load<Sprite>(npc.NpcUnit.ImgPath);
        openNpcDetail.SetActive(true);
        enquiryDialogue.SetActive(true);
    }
    private void OpenBook()
    {
        //打开病历
        AudioManager.Instance.PlayAudio("TurnPage", false);
        UIManager.Instance.OpenBingLiWindow();
        
    }
    private void GoBackyard()
    {
        UIManager.Instance.OpenBackyardWindow();
        this.gameObject.SetActive(false);
    }
    private void PlayTransition()
    {
        // 创建并实例化过场动画Prefab
        GameObject transitionInstance = Instantiate(AnimationPrefab);

        // 获取过场动画Prefab中的Animator组件
        Animator animator = transitionInstance.GetComponent<Animator>();
        // 订阅动画播放完成事件，用于销毁动画Prefab
        if (animator != null)
        {
            //animator.SetTrigger("PlayAnimation");
            
            // 在动画播放完后销毁动画Prefab
            Destroy(transitionInstance, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
    
}
