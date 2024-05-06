using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeOfDay 
{
    Morning_1 = 0,
    Morning_2 = 1,
    Afternoon_1 = 2,
    Afternoon_2 = 3,
    Evening_1 = 4,
    Evening_2 = 5,
    EndOfDay = 6,
    Count = 7,
}

public enum EffectAttributeType 
{
    Yang = 1,
    Yin = 2,
    Re  = 3,
    Han = 4,
}

public class DataManager : Singleton<DataManager>
{
    public TimeOfDay CurrentTime { get; private set; }

    public int Day { get; private set; }

    public int Prestige { get; private set; }

    public int SignTimes { get; private set; }


    protected override void Init()
    {
        var generalSettings = ConfigManager.Instance.GetConfig<GeneralSettingsConfig>(1);
        Prestige = generalSettings.originalPrestige;
        Day = 1;
        CurrentTime = TimeOfDay.Morning_1;
        SignTimes = generalSettings.signTimes;
    }

    public void ResetData()
    {
        Init();
        HerbDataManager.Instance.Reset();
        RecipeDataManager.Instance.Reset();
        HerbPickerFactory.GetHerbPicker().Init();
        NPCDataManager.Instance.ResetNPCData();
        
        Notification.Instance.Notify(Notification.TimeChanged);
        Notification.Instance.Notify(Notification.PrestigeChanged);
    }

    public void MoveToNextTime() 
    {
        CurrentTime = (TimeOfDay)(((int)CurrentTime + 1) % (int)TimeOfDay.Count);

        if (CurrentTime == TimeOfDay.Morning_1) 
        {
            Day++;
            SignTimes = ConfigManager.Instance.GetConfig<GeneralSettingsConfig>(1).signTimes;
            Notification.Instance.Notify(Notification.SignTimesChanged);
            Notification.Instance.Notify(Notification.NextDay);
        }

        Notification.Instance.Notify(Notification.TimeChanged);
    }

    public void ChangePrestige(int change) 
    {
        if (change == 0) 
        {
            return;
        }

        Prestige += change;
        Prestige = Mathf.Max(0, Prestige);

        Notification.Instance.Notify(Notification.PrestigeChanged);

        if (change > 0)
        {
            CommonUtils.ShowTips(new Vector2(-450f, 475f),$"声望+{change}");
        }
    }

    public void ReduceSignTimes()
    {
        SignTimes--;
        Notification.Instance.Notify(Notification.SignTimesChanged);
    }
}
