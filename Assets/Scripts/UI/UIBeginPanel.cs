using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBeginPanel : MonoBehaviour
{
    public Button startBtn;
    
    public Button continueBtn;

    public Button quitBtn;

    private void OnEnable()
    {
        startBtn.onClick.AddListener(OnStartBtnClicked);
        continueBtn.onClick.AddListener(OnContinueBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }

    private void Start()
    {
        AudioManager.Instance.PlayAudio("BGM", true);
    }

    private void OnContinueBtnClicked()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenJiKeWindow();
    }

    private void OnQuitBtnClicked()
    {
        Application.Quit();
    }

    private void OnStartBtnClicked()
    {
        DataManager.Instance.ResetData();
        
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenJiKeWindow();
    }

    private void OnDisable()
    {
        startBtn.onClick.RemoveListener(OnStartBtnClicked);
        quitBtn.onClick.RemoveListener(OnQuitBtnClicked);
        continueBtn.onClick.RemoveListener(OnContinueBtnClicked);
    }
}
