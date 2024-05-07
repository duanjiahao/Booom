using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDaysAnim : MonoBehaviour
{
    public Animator anim;
    public GameObject commonUI;

    public void PlayNewDaysAnim()
    {
        commonUI.SetActive(false);
        anim.gameObject.SetActive(true);
        anim.Play("nd", 0);
    }

    
}
