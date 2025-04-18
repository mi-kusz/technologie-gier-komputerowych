using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log("Dodano do ekwipunku: " + item.itemName);

        FindObjectOfType<InventoryUI>().UpdateInventoryUI();
    }

    public List<ItemData> GetItems()
    {
        return items;
    }

    public bool HasItem(string itemName)
    {
        foreach (ItemData item in items)
        {
            if (item.itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }
    
    public int CountItem(string itemName)
    {
        int count = 0;
        foreach (ItemData item in items)
        {
            if (item.itemName == itemName)
            {
                count++;
            }
        }
        return count;
    }

    public void RemoveItem(string itemName, int count)
    {
        for (int i = items.Count - 1; i >= 0 && count > 0; i--)
        {
            if (items[i].itemName == itemName)
            {
                items.RemoveAt(i);
                count--;
            }
        }

        FindObjectOfType<InventoryUI>().UpdateInventoryUI();
    }
}