using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData
{
    public int npcID;
    public string desc;
    public int[] avoidIDList;
    public int[] needIDList;
    public int[] avoidMailIDList;
    public int[] needMailIDList;
    //最终返回的需求和规避对话ID
    public int[] MatchMailIDList;
    public string imagePath;
}
