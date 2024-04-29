using System.Collections.Generic;
using System.Linq;

public class Inventory<T> where T : IInventoryItem
{
    private List<T> items = new List<T>();

    // 添加物品到背包
    public void AddItem(T item)
    {
        //判断是否存在
        var existingItem = items.FirstOrDefault(i => i.ID == item.ID);
        if (existingItem != null)
        {
            // 如果物品已存在，增加数量
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            // 如果物品不存在，添加新物品
            items.Add(item);
        }
    }

    // 从背包移除物品
    public bool UseItem(int itemId, int quantity)
    {
        var item = items.FirstOrDefault(i => i.ID == itemId);
        if (item != null && item.Quantity >= quantity)
        {
            item.Quantity -= quantity;
            if (item.Quantity == 0)
            {
                //TODO：数量为0时判断药材和药方
                //items.Remove(item);
                return false;
            }
            return true;
        }
        return false;
    }

    // 获取背包中的所有物品
    public List<T> GetAllItems()
    {
        return items;
    }

    // 查询特定物品
    public T GetItem(int itemId)
    {
        return items.FirstOrDefault(i => i.ID == itemId);
    }
}
