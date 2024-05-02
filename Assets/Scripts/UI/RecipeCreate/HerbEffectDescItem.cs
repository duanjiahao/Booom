using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbEffectDescItem : MonoBehaviour
{
    public GameObject effecting;

    public GameObject notEffecting;

    public TextMeshProUGUI goodEffect_effecting;

    public TextMeshProUGUI badEffect_effecting;

    public TextMeshProUGUI num_effecting;

    public TextMeshProUGUI goodEffect_notEffecting;

    public TextMeshProUGUI badEffect_notEffecting;

    public TextMeshProUGUI num_notEffecting;

    public void RefreshUI(EffectAxisConfig goodConfig, EffectAxisConfig badConfig, bool effecting) 
    {
        this.effecting.SetActive(effecting);
        this.notEffecting.SetActive(!effecting);

        this.badEffect_effecting.text = badConfig.name;
        this.badEffect_notEffecting.text = badConfig.name;

        this.goodEffect_effecting.text = goodConfig.name;
        this.goodEffect_notEffecting.text = goodConfig.name;

        this.num_effecting.text = goodConfig.value.ToString();
        this.num_notEffecting.text = goodConfig.value.ToString();
    }
}
