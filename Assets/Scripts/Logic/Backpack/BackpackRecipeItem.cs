using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackRecipeItem : IInventoryItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string[] Effects { get; set; }

    public void InitItemInfo(int id, string name, string des, int quantity, string[] effects)
    {
        this.ID = id;
        this.Name = name;
        this.Description = des;
        this.Quantity = quantity;
        this.Effects = effects;
    }
}
