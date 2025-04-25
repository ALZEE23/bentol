using UnityEngine;

public class FishingEvent : MonoBehaviour
{
    public Animator playerAnimator;

    public void FinishFishing(bool success, FishData fish)
    {
        if (success)
        {
            playerAnimator.SetTrigger("strike");
            Debug.Log("Berhasil dapat: " + fish.fishName);
            // Tambahkan ke inventory, dll.
        }
        else
        {
            playerAnimator.SetTrigger("gagal");
            Debug.Log("Gagal! Ikan lolos: " + fish.fishName);
        }
    }
}
