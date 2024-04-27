using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using FindFunc;
public class HerbManager : MonoBehaviour
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
        // 创建药方背包
        Inventory<BackpackHerbItem> herbInventory = new Inventory<BackpackHerbItem>();
        //读取数据
        TextAsset configFile = Resources.Load<TextAsset>("Configs/HerbsConfig");
        string text = configFile.text;
        if (text != null)
        {
            //反序列化json
            var herbDict = JsonConvert.DeserializeObject<Dictionary<int, HerbsConfig>>(text);
            List<HerbsConfig> herbList = new List<HerbsConfig>(herbDict.Values);
            foreach (var item in herbList)
            {
                var obj = new BackpackHerbItem();
                obj.InitItemInfo(item.id, item.name, item.desc, 0, new int[] { item.attribute1, item.attribute2, item.attribute3, item.attribute4 });
                //将数据加入列表中
                herbInventory.AddItem(obj);
                //Debug.Log($"Item ID: {item.id}, Name: {item.name}, Description: {item.desc}");
            }

        }
        else
        {
            Debug.LogError("Failed to load the config file.");
        }
        if (herbInventory.GetAllItems().Count != 0)
        {
            //实例化每个slot
            foreach (BackpackHerbItem data in herbInventory.GetAllItems())
            {
                tempItem = Instantiate(
                HerbPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
//                tempItem.GetComponent<HerbInfoPanel>().Init();
                //tempItem.GetComponent<PilotInfoItem>().InitItemData(data);
                //            tempItem.GetComponent<PilotInfoItem>().SetItemData(data);

                //            PilotItemList.Add(tempItem);
                //获取icon对象的赋值方法
                tempItem.GetComponentInChildren<HerbIconImage>().SetData(data);
                Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "weight");
                nameText.text = data.Name;
            }
        }


    }

}