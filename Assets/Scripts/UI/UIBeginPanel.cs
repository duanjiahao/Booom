using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBeginPanel : MonoBehaviour
{
    public Button startBtn;

    public Button quitBtn;

    private void OnEnable()
    {
        startBtn.onClick.AddListener(OnStartBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }

    private void OnQuitBtnClicked()
    {
        Application.Quit();
    }

    private void OnStartBtnClicked()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenJiKeWindow();
    }

    private void OnDisable()
    {
        startBtn.onClick.RemoveListener(OnStartBtnClicked);
        quitBtn.onClick.RemoveListener(OnQuitBtnClicked);
    }
}
