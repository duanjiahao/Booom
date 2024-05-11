using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeCreatWindow : MonoBehaviour
{
    public List<HerbEffectAttributeUI> attributeUIs;

    public Button backyardBtn;

    public Button goFront;

    public Toggle toggleHerb;

    public Toggle toggleRecipe;

    public Transform shelves;

    public Transform content;

    public HerbSelectUI herbSelectUI;

    public RecipeSelectUI recipeSelectUI;

    public RectTransform allContent;

    public HerbInfoPanel herbInfoPanel;

    public RecipeInfoUI recipeInfoPanel;

    public IntroductionHelper introductionHelper;
    
    private void OnEnable()
    {
        toggleHerb.onValueChanged.AddListener(OnToggleHerb);
        toggleRecipe.onValueChanged.AddListener(OnToggleRecipe);
        herbSelectUI.resetBtn.onClick.AddListener(OnHerbResetBtnClicked);
        backyardBtn.onClick.AddListener(OnBackyardBtnClicked);
        goFront.onClick.AddListener(OnGoFrontClicked);
        RefreshUI();

        if (!DataManager.Instance.RecipeIntroduction && DataManager.Instance.isIntroductionOn)
        {
            introductionHelper.gameObject.SetActive(true);
            DataManager.Instance.RecipeIntroduction = true;
        }
    }

    private void OnBackyardBtnClicked()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenBackyardWindow();
    }

    private void OnGoFrontClicked()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenJiKeWindow();
    }

    private void OnHerbResetBtnClicked()
    {
        for (int i = 0; i < attributeUIs.Count(); i++)
        {
            var attributeUI = attributeUIs[i];
            attributeUI.InitUI((EffectAttributeType)(i + 1));
        }
        
        RefreshContent(true);
    }

    private void OnToggleRecipe(bool value)
    {
        if (value)
        {
            recipeSelectUI.gameObject.SetActive(true);
            herbSelectUI.gameObject.SetActive(false);
            recipeSelectUI.InitUI();
            RefreshContent(false);
            
            for (int i = 0; i < attributeUIs.Count(); i++)
            {
                var attributeUI = attributeUIs[i];
                attributeUI.InitUI((EffectAttributeType)(i + 1));
            }
        }
    }

    private void OnToggleHerb(bool value)
    {
        if (value)
        {
            recipeSelectUI.gameObject.SetActive(false);
            herbSelectUI.gameObject.SetActive(true);
            herbSelectUI.InitUI();
            RefreshContent(true);
        }
    }

    private void OnDisable()
    {
        toggleHerb.onValueChanged.RemoveListener(OnToggleHerb);
        toggleRecipe.onValueChanged.RemoveListener(OnToggleRecipe);
        herbSelectUI.resetBtn.onClick.RemoveListener(OnHerbResetBtnClicked);
        backyardBtn.onClick.RemoveListener(OnBackyardBtnClicked);
    }

    public void RefreshUI()
    {
        toggleHerb.SetIsOnWithoutNotify(true);
        toggleRecipe.SetIsOnWithoutNotify(false);
        
        herbSelectUI.gameObject.SetActive(true);
        recipeSelectUI.gameObject.SetActive(false);
        
        herbSelectUI.InitUI();

        RefreshContent(true);

        for (int i = 0; i < attributeUIs.Count(); i++)
        {
            var attributeUI = attributeUIs[i];
            attributeUI.InitUI((EffectAttributeType)(i + 1));
        }
    }

    public void RefreshContent(bool herb, RecipeItem selectRecipe = null)
    {
        content.transform.DestroyAllChildren();
        shelves.transform.DestroyAllChildren();
        int count;
        if (herb)
        {
            var herbList = HerbDataManager.Instance.GetAllHerbItems();
            for (int i = 0; i < herbList?.Count; i++)
            {
                var herbData = herbList[i];

                var herbItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/HerbItem"), content);

                var mono = herbItem.GetComponent<HerbUIItem>();
                
                mono.InitUI(herbData);

                var helper = herbItem.GetComponent<HerbItemHoverHelper>();
                helper.herbInfoPanel = herbInfoPanel;
            }

            count = Mathf.CeilToInt((herbList?.Count ?? 0) / 5f);
            allContent.sizeDelta = new Vector2(900f, count * 128f + 40f);
        }
        else
        {
            var recipeList = RecipeDataManager.Instance.GetAllRecipeItems();
            for (int i = 0; i < recipeList?.Count; i++)
            {
                var recipeData = recipeList[i];

                var herbItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/RecipeItem"), content);

                var mono = herbItem.GetComponent<RecipeUIItem>();
                
                mono.InitUI(recipeData);
                if (recipeData.Id == selectRecipe?.Id)
                {
                    mono.SetSelect(true);
                }

                mono.btn.onClick.RemoveAllListeners();
                mono.btn.onClick.AddListener(() =>
                {
                    OnRecipeItemClicked(recipeData, mono);
                });
                
                var helper = herbItem.GetComponent<RecipeHoverHelper>();
                helper.recipeInfoPanel = recipeInfoPanel;
            }
            
            count = Mathf.CeilToInt((recipeList?.Count ?? 0) / 5f);
            allContent.sizeDelta = new Vector2(900f, count * 128f + 40f);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/shelf"), shelves);
        }
    }

    public void OnHerbItemClicked(HerbItem herbItem, HerbUIItem mono)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            var child = content.GetChild(i);
            var childMono = child.GetComponent<HerbUIItem>();
            if (childMono.GetState() == 3)
            {
                childMono.SetState(2);
            }
        }
        mono.SetState(3);
    }
    
    private void OnRecipeItemClicked(RecipeItem recipeItem, RecipeUIItem mono)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            var child = content.GetChild(i);
            var childMono = child.GetComponent<RecipeUIItem>();
            childMono.SetSelect(false);
        }
        mono.SetSelect(true);
        
        recipeSelectUI.RefreshUI(recipeItem);
    }
}
