using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using FindFunc;
//药材背包UI
public class HerbBackpack : MonoBehaviour
{
    //herb list manager
    public GameObject PagingRoot;
    public GameObject HerbPrefab;
    private GameObject tempItem;
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        //FindInfo();
    }
    void FindInfo()
    {
        //<组件类型>（根节点,查询名称）
        //组件类型：是非GameObject的其他组件
        //查询名称：最好同名，这样以后方便维护
        //_idText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "id");


    }
    // Start is called before the first frame update
    void Start()
    {
        List<HerbItem> herbInventory = HerbDataManager.Instance.GetAllHerbItems();
        if (herbInventory.Count != 0)
        {
            //实例化每个slot
            foreach (HerbItem data in herbInventory)
            {
                tempItem = Instantiate(
                HerbPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
                //获取icon对象的赋值方法
                tempItem.GetComponentInChildren<HerbIconImage>().SetData(data);
                Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "weight");
                nameText.text = data.HerbConfig.name;
            }
        }


    }

}