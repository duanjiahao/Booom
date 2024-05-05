using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
//药材信息窗口
public class HerbInfoPanel : MonoBehaviour
{
    public GameObject PagingRoot;
    private Text _herbName;
    private Text _herbDesc;
    public GameObject AttributePrefab;
    public GameObject AttributeUnknownPrefab;
    private List<string> _attributeList = new List<string>();
    private List<Sprite> _attributeImgList = new List<Sprite>();
    public List<Sprite> spriteList = new List<Sprite>();
    private List<GameObject> itemList = new List<GameObject>();
    private void Awake()
    {
        PagingRoot = this.gameObject;
    }
    public void Init()
    {
        _herbName = transform.Find("HerbName").GetComponent<Text>();
        _herbDesc = transform.Find("description").GetComponent<Text>();
    }
    public void SetHerbInfo(HerbItem data)
    {
        foreach(var item in itemList)
        {
            Destroy(item);
        }
        itemList.Clear();
        _attributeList.Clear();
        _attributeImgList.Clear();
        _herbName.text = data.HerbConfig.name;
        _herbDesc.text = data.HerbConfig.desc;
        bool HaveUnknown = false;
        int NowIndex = 0;
        for(int i = 0; i < 4; i++)
        {
            if (data.IsVisible[i])
            {
                if(data.AttributeList[i] != 0)
                {
                    _attributeList.Add(GetAttributeText(data.AttributeList[i]));
                    _attributeImgList.Add(spriteList[i]);
                    NowIndex +=1;
                }
                
            }
            else
            {
                //至少有一个未知
                HaveUnknown = true;   
            }
        }
        if (_attributeList.Count != 0)
        {
            for(int i = 0; i < _attributeList.Count; i++)
            {
                GameObject tempItem = Instantiate(
                    AttributePrefab,
                    transform.position,
                    Quaternion.identity,
                    transform.Find("effectList")
                );
                tempItem.GetComponentInChildren<Text>().text = _attributeList[i];
                tempItem.GetComponentInChildren<Image>().sprite = _attributeImgList[i];
                itemList.Add(tempItem);
            }
        }
        if (HaveUnknown)
        {
            GameObject tempItem = Instantiate(
                    AttributeUnknownPrefab,
                    transform.position,
                    Quaternion.identity,
                    transform.Find("effectList")
                );
            tempItem.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Dynamic/icon_未知");
            itemList.Add(tempItem);
        }
        

    }
    private string GetAttributeText(int num)
    {
        string text = "";
        switch (num)
        {
            case 1:
                text = "+";
                break;
            case 2:
                text = "++";
                break;
            case 3:
                text = "+++";
                break;
            case -1:
                text = "-";
                break;
            case -2:
                text = "--";
                break;
            case -3:
                text = "---";
                break;
        }
        return text;
        
    }
}
