using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 策划的需求文档说，要有7个时间段，UI上显示6个时间段（最后俩个晚上都显示同一个时间段）
// 感觉这样数据和UI不一致会有坑，不确定这个有什么用，先不管，之后和策划确认
public enum TimeOfDay 
{
    Morning_1 = 0,
    Morning_2 = 1,
    Afternoon_1 = 2,
    Afternoon_2 = 3,
    Evening_1 = 4,
    Evening_2 = 5,
    Count = 6,
}

public class DataManager : Singleton<DataManager>
{
    public TimeOfDay CurrentTime { get; private set; }

    public int Day { get; private set; }

    protected override void Init()
    {
        // TODO: 读存档
        Prestige = 0;
        Day = 1;
        CurrentTime = TimeOfDay.Morning_1;
    }

    public void MoveToNextTime() 
    {
        CurrentTime = (TimeOfDay)(((int)CurrentTime + 1) % (int)TimeOfDay.Count);

        if (CurrentTime == TimeOfDay.Morning_1) 
        {
            Day++;
            Notification.Instance.Notify(Notification.NextDay);
        }
    }

    public int Prestige { get; private set; }

    public void ChangePrestige(int change) 
    {
        Prestige += change;
    }
}
