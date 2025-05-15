using UnityEngine;

public class FishingEvent : MonoBehaviour
{
    public Animator playerAnimator;
    public FishData fishData;
    public Transform fishInRod;

    public void FinishFishing(bool success, FishData fish)
    {
        fishData = fish;
        if (success)
        {
            playerAnimator.SetTrigger("strike");
            Instantiate(fish.fishPrefab, fishInRod.position, Quaternion.identity, fishInRod);
            Debug.Log("Berhasil dapat: " + fish.fishName);

            // Update fish card
            fish.getFish = true;
            BookManager bookManager = FindObjectOfType<BookManager>();
            if (bookManager != null)
            {
                bookManager.UnlockFishCard(fish.fishID);
            }
        }
        else
        {
            playerAnimator.SetTrigger("gagal");
            Debug.Log("Gagal! Ikan lolos: " + fish.fishName);
        }
    }
}
