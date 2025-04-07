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
}