using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Text inventoryText;

    public string head;

    public void UpdateInventoryUI()
    {
        List<ItemData> items = InventoryManager.Instance.GetItems();
        inventoryText.text = head + ":\n";

        foreach (ItemData item in items)
        {
            inventoryText.text += item.itemName + "\n";
        }
    }

    void Start()
    {
        UpdateInventoryUI();
    }
}