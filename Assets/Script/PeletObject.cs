using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeletObject : MonoBehaviour
{
    public PeletItem itemData;

    public void OnClick()
    {
        if (itemData == null)
        {
            Debug.LogError("ItemData di PeletObject masih NULL!");
            return;
        }

        bool success = InventoryManager.Instance.AddItem(itemData);
        if (success)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory penuh!");
        }
    }

}
