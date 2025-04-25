using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour, IPointerClickHandler
{
    public PeletItem itemData;
    public Image image;
    private bool isSelected = false;

    public bool isFusion;

    public void SetItemData(PeletItem item)
    {
        itemData = item;
        image.sprite = item.icon;
    }

    public void ClearSlot()
    {
        itemData = null;
        isSelected = false;
        image.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFusion)
        {
            Debug.Log("Item fusion dipilih: " + itemData.itemName);
            return;
        }
        FusionManager fusionManager = FusionManager.Instance;
        if (itemData == null) return;

        if (isSelected || fusionManager.SelectedCount >= 2)
        {
            Debug.Log("Tidak bisa memilih item lagi.");
            return;
        }

        isSelected = true;
        fusionManager.SelectItem(itemData);

        // Tampilkan status visual
        image.color = Color.green;
    }

    public void ResetSelection()
    {
        isSelected = false;
        image.color = Color.white;
    }
}
