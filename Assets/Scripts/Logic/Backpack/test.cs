using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using FindFunc;
public class test : MonoBehaviour
{
    public GameObject PagingRoot;
    private Text _idText;
    private Text _nameText;
    private Text _descText;
    private Text _attriText;
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
        _idText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "id");
        _nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "name");
        _descText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "desc");
        _attriText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "attribute");

    }
    // Start is called before the first frame update
    void Start()
    {
        // 创建药材背包
        Inventory<BackpackHerbItem> herbInventory = new Inventory<BackpackHerbItem>();
        herbInventory.AddItem(new BackpackHerbItem { ID = 1, Name = "Ginseng", Description = "Boosts energy", Quantity = 5, Attribute = new int[] { 1, 2, 3, 4 } });

        // 创建药方背包
        Inventory<BackpackRecipeItem> prescriptionInventory = new Inventory<BackpackRecipeItem>();
        prescriptionInventory.AddItem(new BackpackRecipeItem { ID = 101, Name = "Ginseng + Honey", Description = "wow", Effects = new string[] { "Root", "Leaf", "Stem" }, Quantity = 2 });

        // 输出药材信息
        foreach (var herb in herbInventory.GetAllItems())
        {
            Debug.Log($"Herb: {herb.Name}, Quantity: {herb.Quantity}");
        }

        // 输出药方信息
        foreach (var prescription in prescriptionInventory.GetAllItems())
        {
            Debug.Log($"Prescription: {prescription.Name}, Usage: {prescription.Effects[0]}");
        }

        //读取数据
        TextAsset configFile = Resources.Load<TextAsset>("Configs/HerbsConfig");
        string text = configFile.text;
        Debug.Log(text);
        if (text != null)
        {
            var herbDict = JsonConvert.DeserializeObject<Dictionary<int, HerbsConfig>>(text);
            List<HerbsConfig> herbList = new List<HerbsConfig>(herbDict.Values);
            foreach (var item in herbList)
            {
                var obj = new BackpackHerbItem();
                obj.InitItemInfo(item.id, item.name, item.desc, 0, new int[] { item.attribute1, item.attribute2, item.attribute3, item.attribute4 },item.iconPath);
                herbInventory.AddItem(obj);
                //Debug.Log($"Item ID: {item.id}, Name: {item.name}, Description: {item.desc}");
            }

        }
        else
        {
            Debug.LogError("Failed to load the config file.");
        }

}


}
