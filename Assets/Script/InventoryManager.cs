using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<PeletItem> peletInventory = new List<PeletItem>();
    public int maxSlots = 2;

    public List<InventoryUi> slot = new List<InventoryUi>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool AddItem(PeletItem item)
    {
        Debug.Log("Menambahkan item: " + item.itemName);
        Debug.Log("Inventory count sebelum: " + peletInventory.Count);

        if (peletInventory.Count >= maxSlots)
        {
            Debug.LogWarning("Inventory penuh, tidak bisa tambah!");
            return false;
        }

        peletInventory.Add(item);
        Debug.Log("Inventory count sesudah: " + peletInventory.Count);

        for (int i = 0; i < slot.Count; i++)
        {
            Debug.Log($"Slot {i} isi: {(slot[i] != null ? "Ada" : "NULL")}");
            if (i < peletInventory.Count)
            {
                slot[i].SetItemData(peletInventory[i]);
            }
            else
            {
                slot[i].ClearSlot();
            }
        }
        return true;
    }


    public void RemoveItem(PeletItem item)
    {
        peletInventory.Remove(item);
        // update UI di sini
    }
}

