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

    // Start is called before the first frame update
    void Start()
    {
        Notification.Instance.Register(Notification.PrestigeChanged, RefreshPrestige);
        Notification.Instance.Register(Notification.TimeChanged, RefreshTime);

        RefreshPrestige(null);
        RefreshTime(null);
    }

    private void RefreshTime(object data)
    {
        day.text = DataManager.Instance.Day.ToString();

        var currentTime = DataManager.Instance.CurrentTime;

        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].SetActive((int)currentTime > i);
        }

        for (int i = 0; i < times.Count; i++)
        {
            times[i].SetActive((int)currentTime == i || (currentTime == TimeOfDay.EndOfDay && i == times.Count - 1));
        }
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
