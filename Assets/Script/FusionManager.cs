using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{
    public List<PeletItem> selectedForFusion = new List<PeletItem>();
    public List<FishData> fishDatabase;

    public Animator fusionUiAnimator;
    public Animator playerAnimator;
    public int SelectedCount => selectedForFusion.Count;
    public static FusionManager Instance;
    public ButtonUi inventoryUi;
    public ButtonUi buttonUi;

    public List<InventoryUi> slot;
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
    public bool IsCanFuse()
    {
        return selectedForFusion.Count == 2;
    }

    public void SelectItem(PeletItem item)
    {
        if (selectedForFusion.Contains(item)) return;

        selectedForFusion.Add(item);
        Debug.Log("Item Dipilih: " + item.itemName);

        for (int i = 0; i < slot.Count; i++)
        {
            Debug.Log($"Slot {i} isi: {(slot[i] != null ? "Ada" : "NULL")}");
            if (i < selectedForFusion.Count)
            {
                slot[i].SetItemData(selectedForFusion[i]);
            }
            else
            {
                slot[i].ClearSlot();
            }
        }


    }

    public void FuseItems()
    {
        playerAnimator.SetTrigger("mulai_mancing");
        int id1 = selectedForFusion[0].itemID;
        int id2 = selectedForFusion[1].itemID;

        FishData matchedFish = null;

        foreach (FishData fish in fishDatabase)
        {

            if ((fish.peletID1 == id1 && fish.peletID2 == id2) || (fish.peletID1 == id2 && fish.peletID2 == id1))
            {
                matchedFish = fish;
                break;
            }
        }

        if (matchedFish != null)
        {
            Debug.Log("Jackpot! Dapat ikan: " + matchedFish.fishName);
            playerAnimator.SetTrigger("dapet_ikan");
            QuickTimeEvent qte = FindObjectOfType<QuickTimeEvent>();
            if (qte != null)
            {
                qte.StartQTE(matchedFish);
            }
        }
        else
        {
            Debug.Log("Tidak cocok, masuk mode gacha...");
            QuickTimeEvent qte = FindObjectOfType<QuickTimeEvent>();
            playerAnimator.SetTrigger("dapet_ikan");
            string randomFishName = GetRandomFish();

            FishData randomFish = new FishData();
            randomFish.fishName = randomFishName;
            qte.StartQTE(randomFish);
        }

        InventoryManager.Instance.RemoveItem(selectedForFusion[0]);
        InventoryManager.Instance.RemoveItem(selectedForFusion[1]);


        foreach (var slotUI in slot)
        {
            slotUI.ClearSlot();
        }

        selectedForFusion.Clear();
    }

    private string GetRandomFish()
    {

        string[] possibleFish = new string[] { "Lele", "Gurame", "Nila", "Patin" };
        int rand = Random.Range(0, possibleFish.Length);
        return possibleFish[rand];
    }

    void SpawnIkan(string fishName)
    {

        Debug.Log("Ikan yang didapat: " + fishName);
    }

    public void DeselectItem(PeletItem item)
    {
        if (!selectedForFusion.Contains(item)) return;

        selectedForFusion.Remove(item);
        Debug.Log("Item Dihapus: " + item.itemName);

        for (int i = 0; i < slot.Count; i++)
        {
            if (i < selectedForFusion.Count)
            {
                slot[i].SetItemData(selectedForFusion[i]);
            }
            else
            {
                slot[i].ClearSlot();
            }
        }



    }

}