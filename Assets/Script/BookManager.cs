using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public List<FishData> fishList;
    public Transform fishCardParent;
    private bool hasInstantiatedCards = false;
    private Dictionary<int, GameObject> spawnedCards = new Dictionary<int, GameObject>();

    void Start()
    {
        fishList = FusionManager.Instance.fishDatabase;
        InstantiateFishCards();
    }

    void Update()
    {
        UpdateFishCards();
    }

    private void InstantiateFishCards()
    {
        if (hasInstantiatedCards) return;

        foreach (FishData fish in fishList)
        {
            GameObject card = Instantiate(fish.fishCardDefaultPrefab, fishCardParent);
            spawnedCards.Add(fish.fishID, card);
        }

        hasInstantiatedCards = true;
    }

    private void UpdateFishCards()
    {
        foreach (FishData fish in fishList)
        {
            if (fish.getFish && spawnedCards.ContainsKey(fish.fishID))
            {
                
                GameObject oldCard = spawnedCards[fish.fishID];
                if (oldCard.CompareTag("DefaultCard")) 
                {
                   
                    GameObject newCard = Instantiate(fish.fishCardPrefab, oldCard.transform.position,
                                                   Quaternion.identity, fishCardParent);
                    Destroy(oldCard);
                    spawnedCards[fish.fishID] = newCard;
                }
            }
        }
    }


    public void UnlockFishCard(int fishID)
    {
        FishData fish = fishList.Find(f => f.fishID == fishID);
        if (fish != null)
        {
            fish.getFish = true;
        }
    }
}
