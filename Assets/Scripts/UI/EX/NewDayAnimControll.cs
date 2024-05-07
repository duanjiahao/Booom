using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDayAnimControll : MonoBehaviour
{
    public GameObject commonUI;
    public jiekePanel jkp;
    public void EndOfPlay()
    {

       this.gameObject.SetActive(false);
        commonUI.SetActive(true);
    }

    public void ToNextTime()
    {
        DataManager.Instance.MoveToNextTime();
        jkp.RefreshPanelBg();
    }

    public void PlayCandelVoice()
    {
        AudioManager.Instance.PlayAudio("BlowTheCandel", false);
    }    

    public void PlayChickenVoice()
    {
        AudioManager.Instance.PlayAudio("Chicken", false);
    }
}
