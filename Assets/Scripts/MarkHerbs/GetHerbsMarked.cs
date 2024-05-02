using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHerbsMarked : MonoBehaviour
{

    public GameObject herbsInfo;
    public Texture2D cursorTextureMark;

    public HerbItem currentData;

    public ChangeHerbAttributeValue markValue;

    private StartMarkMode smm;

    private CommonTips ct;

    public string successTips;
    public string failTips;

    private void Awake()
    {
        markValue = transform.Find("HerbsInfo/attributeList/attributeUnknown").GetComponent<ChangeHerbAttributeValue>();
        smm = FindObjectOfType<StartMarkMode>();
        ct = FindObjectOfType<CommonTips>();
    }
    //赋值
    public void GetHerbs(HerbItem data)
    {
        currentData = data;
        CursorBack();
        herbsInfo = transform.Find("HerbsInfo").gameObject;

        Texture2D tex = LoadTextureFromFile(currentData.HerbConfig.iconPath);
        if (tex != null)
        {
            // 创建 Sprite 对象
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

            // 将 Sprite 赋值给 Image 组件
            herbsInfo.transform.Find("icon").GetComponent<Image>().sprite = sprite;
        }
        else
        {
            Debug.LogError("加载图片失败：" + currentData.HerbConfig.iconPath);
        }

        //加载名字描述等数据
        herbsInfo.transform.Find("name").GetComponent<TextMeshProUGUI>().SetText(currentData.HerbConfig.name);
        herbsInfo.transform.Find("desc").GetComponent<TextMeshProUGUI>().SetText(currentData.HerbConfig.desc);

        GameObject attributeList = herbsInfo.transform.Find("attributeList").gameObject;

        //给标记功能的显示
        attributeList.transform.Find("attributeUnknown").GetComponent<ChangeHerbAttributeValue>().getHerbsVisbleAttribute(currentData);

        //显示属性
        for (int i = 0; i < currentData.AttributeList.Length; i++)
        {
            if (currentData.AttributeList[i] != 0 && currentData.IsVisible[i])
            {
                attributeList.transform.Find("attribute"+(i+1)).gameObject.SetActive(true);
                switch (currentData.AttributeList[i])
                {
                    case -3:
                        attributeList.transform.Find("attribute" + (i + 1)).Find("value").GetComponent<TextMeshProUGUI>().SetText("---");
                        break;
                    case -2:
                        attributeList.transform.Find("attribute" + (i + 1)).Find("value").GetComponent<TextMeshProUGUI>().SetText("--");
                        break;
                    case -1:
                        attributeList.transform.Find("attribute" + (i + 1)).Find("value").GetComponent<TextMeshProUGUI>().SetText("-");
                        break;
                    case 1:
                        attributeList.transform.Find("attribute" + (i + 1)).Find("value").GetComponent<TextMeshProUGUI>().SetText("+");
                        break;
                    case 2:
                        attributeList.transform.Find("attribute" + (i + 1)).Find("value").GetComponent<TextMeshProUGUI>().SetText("++");
                        break;
                    case 3:
                        attributeList.transform.Find("attribute" + (i + 1)).Find("value").GetComponent<TextMeshProUGUI>().SetText("+++");
                        break;
                }

            }
            else
            {
                attributeList.transform.Find("attribute" + (i + 1)).gameObject.SetActive(false);
            }
        }


    }


    //获取文件路径
    private Texture2D LoadTextureFromFile(string path)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (System.IO.File.Exists(Application.dataPath +"/" +path+".png"))
        {
            fileData = System.IO.File.ReadAllBytes(Application.dataPath + "/" + path+".png");
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); // 将文件数据加载到 Texture2D
        }
        else
        {
            Debug.LogError("文件不存在：" + Application.dataPath+ "/" + path+ ".png");
        }

        return tex;
    }

    //还原鼠标
    private void CursorBack()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    //取消标记这个药材
    public void CancelMarkThisHerb()
    {
        Cursor.SetCursor(cursorTextureMark, Vector2.zero, CursorMode.Auto);
        this.gameObject.SetActive(false);
    }

    //确定标记这个药材
    public void ConfirmMarkThisHerb()
    {
        if (currentData.AttributeList[markValue.attribute] == markValue.attributeValue && !currentData.IsVisible[markValue.attribute])
        {
            currentData.SetAttributeVisible((EffectAttributeType)markValue.attribute + 1);
            this.gameObject.SetActive(false);
            smm.CancelMarkMode();
            ct.GetTipsText(successTips);
        }
        else
        {
            this.gameObject.SetActive(false);
            smm.CancelMarkMode();
            ct.GetTipsText(failTips);
        }

    }
        

}
