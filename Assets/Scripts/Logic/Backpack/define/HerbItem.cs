using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbItem
{
    public HerbsConfig HerbConfig { get; set; }
    public int Quantity { get; set; }
    public int[] Attribute { get; set; }

    public void InitItemInfo(HerbsConfig herbConfig, int quantity, int[] attribute)
    {
        this.HerbConfig = herbConfig;
        this.Quantity = quantity;
        this.Attribute = attribute;

    }
}
