using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPanel : MonoBehaviour
{
    public Button backBtn;

    public Button returnToStartBtn;

    public Scrollbar slider;

    private void OnEnable()
    {
        backBtn.onClick.AddListener(OnBackBtnClick);
        returnToStartBtn.onClick.AddListener(OnReturnClicked);
    }

    private void OnReturnClicked()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenBeginWindow();
    }

    private void OnBackBtnClick()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
    }
}
