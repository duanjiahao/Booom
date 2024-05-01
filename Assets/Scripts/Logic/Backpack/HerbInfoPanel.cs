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
    private List<Text> _attributeList = new List<Text>();
    private List<Image> _attributeImgList = new List<Image>();
    private void Awake()
    {
        PagingRoot = this.gameObject;
    }
    public void Init()
    {
        _herbName = transform.Find("HerbName").GetComponent<Text>();
        _herbDesc = transform.Find("description").GetComponent<Text>();
        //四个属性的数值（用+/-表示）
        
        _attributeList.Add(UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "Attribute1Text"));
        _attributeList.Add(UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "Attribute2Text"));
        _attributeList.Add(UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "Attribute3Text"));
        _attributeList.Add(UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "Attribute4Text"));
        //四个属性的图片
        _attributeImgList.Add(UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "Attribute1Img"));
        _attributeImgList.Add(UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "Attribute2Img"));
        _attributeImgList.Add(UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "Attribute3Img"));
        _attributeImgList.Add(UnityHelper.GetTheChildNodeComponetScripts<Image>(PagingRoot, "Attribute4Img"));

    }
    public void SetHerbInfo(HerbItem data)
    {
        _herbName.text = data.HerbConfig.name;
        _herbDesc.text = data.HerbConfig.desc;
        for(int i = 0; i < 4; i++)
        {
            if (data.IsVisible[i])
            {
                _attributeList[i].text = data.AttributeList[i].ToString();
            }
            else
            {
                _attributeList[i].text = "";
                _attributeImgList[i].sprite = Resources.Load<Sprite>("Dynamic/icon_未知");
            }
        }

    }
}
