using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBeFound : MonoBehaviour
{
    public int id;


    public void ShowGuestMessage(bool good)
    {
        if(good)
        {
            transform.Find("Need").gameObject.SetActive(true);
            transform.Find("Avoid").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Need").gameObject.SetActive(false);
            transform.Find("Avoid").gameObject.SetActive(true);
        }
    }

    public void NotToShow()
    {
        transform.Find("Need").gameObject.SetActive(false);
        transform.Find("Avoid").gameObject.SetActive(false);
    }
}
