using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 创建药材背包
        Inventory<BackpackHerbItem> herbInventory = new Inventory<BackpackHerbItem>();
        herbInventory.AddItem(new BackpackHerbItem { ID = 1, Name = "Ginseng", Description = "Boosts energy", Quantity = 5, Attribute = new int[] { 1, 2, 3, 4 } });

        // 创建药方背包
        Inventory<BackpackRecipeItem> prescriptionInventory = new Inventory<BackpackRecipeItem>();
        prescriptionInventory.AddItem(new BackpackRecipeItem { ID = 101, Name = "Ginseng + Honey", Description = "wow", Effects = new string[] { "Root", "Leaf", "Stem" }, Quantity = 2 });

        // 输出药材信息
        foreach (var herb in herbInventory.GetAllItems())
        {
            Debug.Log($"Herb: {herb.Name}, Quantity: {herb.Quantity}");
        }

        // 输出药方信息
        foreach (var prescription in prescriptionInventory.GetAllItems())
        {
            Debug.Log($"Prescription: {prescription.Name}, Usage: {prescription.Effects[0]}");
        }
    }


}
