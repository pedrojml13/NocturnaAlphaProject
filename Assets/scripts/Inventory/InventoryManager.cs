using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventoryItem> inventory = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData itemData, int quantity)
    {
        InventoryItem existingItem = inventory.Find(i => i.itemData == itemData);
        if (existingItem != null && itemData.isStackable)
        {
            existingItem.quantity += quantity;
            if (existingItem.quantity > itemData.maxStack)
                existingItem.quantity = itemData.maxStack;
        }
        else
        {
            inventory.Add(new InventoryItem(itemData, quantity));
        }

        // Aquí puedes actualizar UI
    }

    public void RemoveItem(ItemData itemData, int quantity)
    {
        InventoryItem item = inventory.Find(i => i.itemData == itemData);
        if (item != null)
        {
            item.quantity -= quantity;
            if (item.quantity <= 0)
                inventory.Remove(item);
        }

        // Aquí puedes actualizar UI
    }
}
