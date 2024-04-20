using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailData
{
    public int mailID;
    public string mailDesc;
    public int mailType;
}
public class MailTitle
{
    public int titleID;
    public string mailTitle;
    public int titleType;
}
public class MailContent
{
    public int contentID;
    public string mailContent;
    public int contentType;
    public int effectID;
}
public class MailBonus
{
    public int bonusID;
    public List<HerbData> bonusContent;
    public int[] priceList;
    public float weight;
}