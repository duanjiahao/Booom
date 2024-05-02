using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeHerbAttributeValue : MonoBehaviour
{

    public TextMeshProUGUI value;
    public Image icon;

    public int attribute;
    public int attributeValue;

    private int currentAttributeCount;

    //�洢�ĸ�ͼƬ
    public Sprite attributeyang;
    public Sprite attributeyin;
    public Sprite attributere;
    public Sprite attributehan;

    //����ʾ����
    public List<int> canShowAttribute = new List<int>();

    private void Awake()
    {
        value = transform.Find("value").gameObject.GetComponent<TextMeshProUGUI>();
        icon = transform.Find("attributeicon").gameObject.GetComponent<Image>();
    }

    public void initialization()
    {
        attribute = canShowAttribute[0];
        attributeValue = 1;
        currentAttributeCount = 1;
        AttributeShow();
    }

    //��ֵ����
    public void ValueUp()
    {
        switch (attributeValue)
        {
            case 1:
                attributeValue = 2;
                break;
            case 2:
                attributeValue = 3;
                break;
            case 3:
                attributeValue = -3;
                break;
            case -3:
                attributeValue = -2;
                break;
            case -2:
                attributeValue = -1;
                break;
            case -1:
                attributeValue = 1;
                break;
        }
        AttributeShow();
    }

    //��ֵ����
    public void ValueDown()
    {
        switch (attributeValue)
        {
            case 1:
                attributeValue = -1;
                break;
            case 2:
                attributeValue = 1;
                break;
            case 3:
                attributeValue = 2;
                break;
            case -3:
                attributeValue = 3;
                break;
            case -2:
                attributeValue = -3;
                break;
            case -1:
                attributeValue = -2;
                break;
        }
        AttributeShow();
    }

    //������ǰ
    public void AttributeUp()
    {

        currentAttributeCount += 1;

        if(currentAttributeCount > canShowAttribute.Count)
        {
            attribute = canShowAttribute[0];
            currentAttributeCount = 1;
        }
        else
        {
            attribute = canShowAttribute[currentAttributeCount - 1];
        }
        AttributeShow();

    }

    //�������
    public void AttributeDown()
    {
        currentAttributeCount -= 1;

        if (currentAttributeCount == 0)
        {
            attribute = canShowAttribute[canShowAttribute.Count-1];
            currentAttributeCount = canShowAttribute.Count;
        }
        else
        {
            attribute = canShowAttribute[currentAttributeCount - 1];
        }

        AttributeShow();

    }



    //�����Ժ�����ֵӳ�䵽��ʾ��
    private void AttributeShow()
    {
        switch (attribute)
        {
            case 0:
                icon.sprite = attributeyang;
                break;
            case 1:
                icon.sprite = attributeyin;
                break;
            case 2:
                icon.sprite = attributere;
                break;
            case 3:
                icon.sprite = attributehan;
                break;
        }

        switch (attributeValue)
        {
            case 1:
                value.SetText("+");
                break;
            case 2:
                value.SetText("++");
                break;
            case 3:
                value.SetText("+++");
                break;
            case -1:
                value.SetText("-");
                break;
            case -2:
                value.SetText("--");
                break;
            case -3:
                value.SetText("---");
                break;
        }

    }

    //��ȡ������ʾ������
    public void getHerbsVisbleAttribute(HerbItem data)
    {
        canShowAttribute.Clear();
        for (int i = 0; i < data.IsVisible.Length; i++)
        {
            if(data.AttributeList[i] == 0 || !data.IsVisible[i])
            {
                canShowAttribute.Add(i);
            }
        }
        initialization();
    }


}
