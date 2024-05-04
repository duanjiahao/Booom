using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackyardWindow : MonoBehaviour
{
    public Button pickBtn;

    public Button createBtn;

    public Button peopleBtn;

    public GameObject anime;

    private void OnEnable()
    {
        pickBtn.gameObject.SetActive(true);
        createBtn.gameObject.SetActive(true);
        peopleBtn.gameObject.SetActive(true);
        anime.SetActive(false);
        
        pickBtn.onClick.AddListener(OnPickBtnClicked);
        createBtn.onClick.AddListener(OnCreateBtnClicked);
        peopleBtn.onClick.AddListener(OnPeopleBtnClicked);
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
            return;
        }

        pickBtn.gameObject.SetActive(false);
        createBtn.gameObject.SetActive(false);
        peopleBtn.gameObject.SetActive(false);
        anime.SetActive(true);

        DOTween.Sequence().InsertCallback(5f, () =>
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
