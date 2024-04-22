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

}
