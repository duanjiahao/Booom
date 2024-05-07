using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackyardWindow : MonoBehaviour
{
    public Button pickBtn;

    public Button createBtn;

    public Button peopleBtn;

    public GameObject anime;

    public GameObject mask;

    public TextMeshProUGUI need;

    public GameObject commonUI;
    
    public IntroductionHelper introductionHelper;

    private void OnEnable()
    {
        pickBtn.gameObject.SetActive(true);
        createBtn.gameObject.SetActive(true);
        peopleBtn.gameObject.SetActive(true);
        anime.SetActive(false);
        mask.SetActive(false);
        commonUI.SetActive(true);

        pickBtn.onClick.AddListener(OnPickBtnClicked);
        createBtn.onClick.AddListener(OnCreateBtnClicked);
        peopleBtn.onClick.AddListener(OnPeopleBtnClicked);

        var isFree = HerbPickerFactory.GetHerbPicker().IsFreeThisTime();
        if (isFree)
        {
            need.text = "免费";
        }
        else
        {
            var generalSettings = ConfigManager.Instance.GetConfig<GeneralSettingsConfig>(1);
            
            var surfix = string.Empty;
            var prefix = string.Empty;
            if (DataManager.Instance.Prestige < generalSettings.prestigeCost) 
            {
                prefix = "<color=#FF2C2C>";
                surfix = "</color>";
            }
            
            need.text = $"{prefix}-{generalSettings.prestigeCost}{surfix}声望";
        }
        
        if (!DataManager.Instance.BackyardIntroduction)
        {
            introductionHelper.gameObject.SetActive(true);
            DataManager.Instance.BackyardIntroduction = true;
        }
    }

    private void OnPeopleBtnClicked()
    {
        UIManager.Instance.OpenJiKeWindow();
        this.gameObject.SetActive(false);
    }

    private void OnCreateBtnClicked()
    {
        UIManager.Instance.OpenRecipeWindow();
        this.gameObject.SetActive(false);
    }

    private void OnPickBtnClicked()
    {
        if (!HerbPickerFactory.GetHerbPicker().CanPick())
        {
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText("声望不足无法采药");
            return;
        }

        if (DataManager.Instance.CurrentTime == TimeOfDay.EndOfDay)
        {
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText("太晚了，明天再去采药吧");
            return;
        }

        commonUI.SetActive(false);
        pickBtn.gameObject.SetActive(false);
        createBtn.gameObject.SetActive(false);
        peopleBtn.gameObject.SetActive(false);
        anime.SetActive(true);
        mask.SetActive(true);

        DOTween.Sequence().InsertCallback(4.5f, () =>
        {
            this.gameObject.SetActive(false);
            UIManager.Instance.OpenCollectionWindow();
        });
    }

    private void OnDisable()
    {
        pickBtn.onClick.RemoveListener(OnPickBtnClicked);
        createBtn.onClick.RemoveListener(OnCreateBtnClicked);
        peopleBtn.onClick.RemoveListener(OnPeopleBtnClicked);
    }
}
