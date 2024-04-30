using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HerbInfoPanel : MonoBehaviour
{
    //背包-草药列表的消息弹窗
    private Text _herbName;
    private Text _herbDesc;
    public GameObject textPrefab;
    private List<GameObject> _textList;
    private GameObject tempItem;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init()
    {
        _herbName = transform.Find("HerbName").GetComponent<Text>();
        _herbDesc = transform.Find("description").GetComponent<Text>();
        if (_textList==null)
        {
            _textList = new List<GameObject>();
        }

    }
    public void SetHerbInfo(HerbItem data)
    {
        _herbName.text = data.HerbConfig.name;
        _herbDesc.text = data.HerbConfig.desc;
        if (_textList.Count != 0)
        {
            foreach(var item in _textList)
            {
                Destroy(item);
            }
        }
        foreach (var dat in data.Attribute)
        {
            tempItem = Instantiate(
                textPrefab,
                transform.position,
                Quaternion.identity,
                transform.Find("effectList")
            );
            tempItem.GetComponent<Text>().text = dat.ToString();
            _textList.Add(tempItem);
        }

    }
}
