using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHerbEffectDesc : MonoBehaviour
{
    public TextMeshProUGUI goodEffect;

    public TextMeshProUGUI badEffect;

    public void RefreshUI(EffectAxisConfig goodConfig, EffectAxisConfig badConfig, bool visible)
    {
        if (visible)
        {
            goodEffect.text = goodConfig.name;
            goodEffect.color = new Color(110f/255f, 212f/255f, 255f/255f);
            badEffect.text = badConfig.name;
        }
        else
        {
            goodEffect.text = "未知";
            goodEffect.color = Color.white;
            badEffect.text = string.Empty;
        }
    }
}
