using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMarkScripts : MonoBehaviour
{
    public Button penButton;

    private void Awake()
    {
        penButton = GameObject.Find("penBtn").GetComponent<Button>();
    }

    public void Start()
    {
        penButton.onClick.AddListener(GetComponent<StartMarkMode>().ChangeToMarkMode);
    }



}
