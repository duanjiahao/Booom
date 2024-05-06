using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public Image bg;
    
    public TextMeshProUGUI text;

    private const float AutoDestroyTime = 3f;

    private const float FadeTime = 0.5f;

    private void OnEnable()
    {
        var s = DOTween.Sequence();

        s.Append(bg.DOFade(1f, FadeTime).From(0f));

        s.Insert(0f, bg.GetComponent<RectTransform>().DOAnchorPosY(0f, FadeTime));
        
        s.InsertCallback(AutoDestroyTime, () =>
        {
            GameObject.Destroy(this.gameObject);
        });
    }

    public void SetContent(string content)
    {
        text.text = content;
    }
}
