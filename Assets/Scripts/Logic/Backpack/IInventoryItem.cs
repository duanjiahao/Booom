using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//药材和药方通用的接口
public interface IInventoryItem
{
    int ID { get; set; }
    int Quantity { get; set; }
}
