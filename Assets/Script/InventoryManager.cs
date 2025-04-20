using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<PeletItem> peletInventory = new List<PeletItem>();
    public int maxSlots = 3;

    public List<InventoryUi> slot = new List<InventoryUi>();

    void Awake() { Instance = this; }

    public bool AddItem(PeletItem item)
    {
        if (peletInventory.Count >= maxSlots) return false;
        peletInventory.Add(item);
        for (int i = 0; i < slot.Count; i++)
        {
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

