using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommonTips : MonoBehaviour
{

    public Image tipsBg;
    public TextMeshProUGUI tipsText;

    public float displayTime;

    private void Awake()
    {
        tipsBg = transform.Find("TipsImage").GetComponent<Image>();
        tipsText = transform.Find("TipsImage/Text").GetComponent<TextMeshProUGUI>();
    }

    public void GetTipsText(string text)
    {
        tipsBg.gameObject.SetActive(true);

        tipsText.SetText(text);
        float textWidth = tipsText.preferredWidth;
        RectTransform rectTransform = tipsText.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(textWidth, rectTransform.sizeDelta.y);

        RectTransform bgRectTransform = tipsBg.GetComponent<RectTransform>();
        bgRectTransform.sizeDelta = new Vector2(textWidth+80, bgRectTransform.sizeDelta.y);

        StartCoroutine(ShowAndHideText());

    }


    IEnumerator ShowAndHideText()
    {
        // �ȴ�ָ��ʱ��
        yield return new WaitForSeconds(displayTime);

        // �ر��ı��ͱ���
        tipsBg.gameObject.SetActive(false);
    }


}
