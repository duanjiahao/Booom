using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPanel : MonoBehaviour
{
    public Button backBtn;

    public Button returnToStartBtn;

    public Scrollbar slider;

    public Button upBtn;

    public Button downBtn;

    public TextMeshProUGUI volume;

    public Toggle MaxWindow;

    private void OnEnable()
    {
        backBtn.onClick.AddListener(OnBackBtnClick);
        returnToStartBtn.onClick.AddListener(OnReturnClicked);
        upBtn.onClick.AddListener(OnUpBtnClicked);
        downBtn.onClick.AddListener(OnDownBtnClicked);

        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener(OnValueChanged);

        MaxWindow.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
        MaxWindow.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool arg0)
    {
        if (arg0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void OnDownBtnClicked()
    {
        slider.value -= 0.01f;
    }

    private void OnUpBtnClicked()
    {
        slider.value += 0.01f;
    }

    private void OnValueChanged(float value)
    {
        AudioListener.volume = value;

        volume.text = $"{Mathf.FloorToInt(value * 100)}";
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
        backBtn.onClick.RemoveListener(OnBackBtnClick);
        returnToStartBtn.onClick.RemoveListener(OnReturnClicked);
        upBtn.onClick.RemoveListener(OnUpBtnClicked);
        downBtn.onClick.RemoveListener(OnDownBtnClicked);
        slider.onValueChanged.RemoveListener(OnValueChanged);
        MaxWindow.onValueChanged.RemoveListener(OnValueChanged);
    }
}
