using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{
    public List<PeletItem> selectedForFusion = new List<PeletItem>();
    public Animator fusionUiAnimator;
    public int SelectedCount => selectedForFusion.Count;
    public static FusionManager Instance;
    public ButtonUi inventoryUi;
    public ButtonUi buttonUi;
    public bool isFusion;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        isFusion = true;
    }

    void Update()
    {

        if (selectedForFusion.Count == 2 && isFusion)
        {
            buttonUi.isFusion = isFusion;
        }
    }

    public void SelectItem(PeletItem item)
    {
        if (selectedForFusion.Contains(item)) return;

        selectedForFusion.Add(item);
        Debug.Log("Item Dipilih: " + item.itemName);

        
    }

    public void FuseItems()
    {
        // logika gabung bisa dari kombinasi ID atau nama
        var fusedItem = new PeletItem();
        fusedItem.itemName = "Pelet Gabungan";
        fusedItem.itemID = 99;

        InventoryManager.Instance.RemoveItem(selectedForFusion[0]);
        InventoryManager.Instance.RemoveItem(selectedForFusion[1]);
        InventoryManager.Instance.AddItem(fusedItem);

        selectedForFusion.Clear();
        // update UI
    }
}

