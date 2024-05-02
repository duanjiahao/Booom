using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindFunc;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//药方tips窗口，仅用于显示，不做任何操作
public class RecipeInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PagingRoot;
    public Text RecipeName;
    public GameObject HerbUnitPrefab;
    public GameObject EffectUnitPrefab;
    private List<GameObject> _herbList;
    private List<GameObject> _effectList;
    private GameObject tempItem;
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    void FindInfo()
    {
        //<组件类型>（根节点,查询名称）
        //组件类型：是非GameObject的其他组件
        //查询名称：最好同名，这样以后方便维护
        RecipeName = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "RecipeName");

    }

    public void Init()
    {
        HerbUnitPrefab = Resources.Load<GameObject>("Prefab/Backpack/InfoPanelComponents/herbItem");
        EffectUnitPrefab = Resources.Load<GameObject>("Prefab/Backpack/InfoPanelComponents/effectItem");
        if (_herbList == null)
        {
            _herbList = new List<GameObject>();
        }
        if (_effectList == null)
        {
            _effectList = new List<GameObject>();
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        this.gameObject.SetActive(true);  // Show the tooltip window
    }
    public void OnPointerExit(PointerEventData eventData)
    {

        this.gameObject.SetActive(false);  // Hide the tooltip window
    }
    public void SetInfoPanelData(RecipeItem data)
    {
        //药方名
        RecipeName.text = data.Name;
        //添加药方中的药材数据
        //先清空
        if (_herbList.Count != 0)
        {
            foreach (var item in _herbList)
            {
                Destroy(item);
            }
        }
        foreach (var dat in data.HerbList)
        {
            tempItem = Instantiate(
                HerbUnitPrefab,
                transform.position,
                Quaternion.identity,
                transform.Find("herbList")
            );
            Text herbName = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "herbName");
            Text herbQuan = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "herbQuan");
            herbName.text = ConfigManager.Instance.GetConfig<HerbsConfig>(dat.HerbId).name;
            herbQuan.text = dat.Weight.ToString();
            _herbList.Add(tempItem);
        }
        //添加药方中的效果数据
        if (_effectList.Count != 0)
        {
            foreach (var item in _effectList)
            {
                Destroy(item);
            }
        }

        var effectList = data.GetEffectList();
        foreach (var dat in effectList)
        {
            tempItem = Instantiate(
                EffectUnitPrefab,
                transform.position,
                Quaternion.identity,
                transform.Find("effectList")
            );
            Text effectName = tempItem.GetComponent<Text>();
            effectName.text = dat.EffectAxisConfig.name;
            _effectList.Add(tempItem);
        }
    }
}
