using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class CommonUI : MonoBehaviour
{
    public TextMeshProUGUI day;

    public List<GameObject> boxes;

    public List<GameObject> times;

    public TextMeshProUGUI prestige;

    public Slider prestigeSlider;

    public TextMeshProUGUI prestigeValue;

    public Button settingBtn;

    public Button introductionBtn;

    // Start is called before the first frame update
    void Start()
    {
        Notification.Instance.Register(Notification.PrestigeChanged, RefreshPrestige);
        Notification.Instance.Register(Notification.TimeChanged, RefreshTime);

        RefreshPrestige(null);
        RefreshTime(null);
        
        settingBtn.onClick.AddListener(OnSettingBtnClicked);
        introductionBtn.onClick.AddListener(OnIntroductionClicked);
    }

    private void OnIntroductionClicked()
    {
        if (UIManager.Instance.backyardWindow.gameObject.activeSelf)
        {
            UIManager.Instance.backyardWindow.introductionHelper.gameObject.SetActive(true);
            return;
        }
        
        if (UIManager.Instance.recipeWindow.gameObject.activeSelf)
        {
            UIManager.Instance.recipeWindow.introductionHelper.gameObject.SetActive(true);
            return;
        }
        
        if (UIManager.Instance.jieKePanel.gameObject.activeSelf)
        {
            UIManager.Instance.jieKePanel.introductionHelper.gameObject.SetActive(true);
        }
    }

    private void OnSettingBtnClicked()
    {
        UIManager.Instance.OpenSettingWindow();
    }

    private void RefreshTime(object data)
    {
        day.text = DataManager.Instance.Day.ToString();

        var currentTime = DataManager.Instance.CurrentTime;

        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].SetActive((int)currentTime > i);
        }

        var isMorning = currentTime is TimeOfDay.Morning_1 or TimeOfDay.Morning_2;
        var isAfternoon = currentTime is TimeOfDay.Afternoon_1 or TimeOfDay.Afternoon_2;
        var isEvening = currentTime is TimeOfDay.Evening_1 or TimeOfDay.Evening_2 or TimeOfDay.EndOfDay;
        
        times[0].SetActive(isMorning);
        times[1].SetActive(!isMorning);
        
        times[2].SetActive(isAfternoon);
        times[3].SetActive(!isAfternoon);
        
        times[4].SetActive(isEvening);
        times[5].SetActive(!isEvening);
    }

    private void RefreshPrestige(object data)
    {
        var presti = DataManager.Instance.Prestige;

        var prestigeConfig = CommonUtils.GetCurrentPrestigeConfigWithNext(out var nextConfig);

        prestige.text = prestigeConfig.levelName;

        var maxValue = nextConfig == null ? prestigeConfig.lowerLimit : nextConfig.lowerLimit;

        var sliderValue = Mathf.Clamp01((float)presti / maxValue);

        prestigeSlider.value = sliderValue;

        prestigeValue.text = $"{presti}/{maxValue}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
