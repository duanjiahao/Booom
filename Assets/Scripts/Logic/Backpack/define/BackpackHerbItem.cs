using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackHerbItem : IInventoryItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public int[] Attribute { get; set; }
    public string Iconpath { get; set; }
    public void InitItemInfo(int id, string name, string des, int quantity, int[] attribute, string iconpath)
    {
        this.ID = id;
        this.Name = name;
        this.Description = des;
        this.Quantity = quantity;
        this.Attribute = attribute;
        this.Iconpath = iconpath;
    }
}
