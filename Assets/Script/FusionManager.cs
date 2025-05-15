using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FusionManager : MonoBehaviour
{
    public List<PeletItem> selectedForFusion = new List<PeletItem>();
    public List<FishData> fishDatabase;

    [Header("Audio Settings")]
    public AudioClip fishingRodSound;
    public AudioMixer audioMixer; // Add reference to your audio mixer
    public string mixerGroupName = "SFX"; // Name of your mixer group
    private AudioSource audioSource;
    private bool isFishing = false;

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

        // Set up AudioSource with mixer group
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = fishingRodSound;
        audioSource.loop = true;

        // Get the audio mixer group and assign it
        AudioMixerGroup[] mixerGroups = audioMixer.FindMatchingGroups(mixerGroupName);
        if (mixerGroups.Length > 0)
        {
            audioSource.outputAudioMixerGroup = mixerGroups[0];
        }
        else
        {
            Debug.LogWarning($"Could not find audio mixer group: {mixerGroupName}");
        }
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
        // StartFishingSound(); // Start the sound when fishing begins
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

            // Get random fish from database
            FishData randomFish = GetRandomFishFromDatabase();
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

    private FishData GetRandomFishFromDatabase()
    {
        if (fishDatabase == null || fishDatabase.Count == 0)
        {
            Debug.LogError("Fish database is empty or null!");
            return null;
        }

        // Get random fish from first two entries in database
        int randomIndex = Random.Range(0, 2); // This will return either 0 or 1
        return fishDatabase[randomIndex];
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

    public void SetVolume(float volume)
    {
        // Convert linear volume to decibels
        float dB = volume > 0 ? 20f * Mathf.Log10(volume) : -80f;
        audioMixer.SetFloat($"{mixerGroupName}Volume", dB);
    }

    public void StartFishingSound()
    {
        isFishing = true;
        audioSource.Play();
        // Optional: Fade in the sound
        StartCoroutine(FadeInSound());
    }

    public void StopFishingSound()
    {
        isFishing = false;
        // Optional: Fade out the sound
        StartCoroutine(FadeOutSound());
    }

    private IEnumerator FadeInSound()
    {
        float currentVolume = 0f;
        audioSource.volume = currentVolume;

        while (currentVolume < 1f)
        {
            currentVolume += Time.deltaTime * 2f; // Adjust fade speed here
            audioSource.volume = currentVolume;
            yield return null;
        }
    }

    private IEnumerator FadeOutSound()
    {
        float currentVolume = audioSource.volume;

        while (currentVolume > 0f)
        {
            currentVolume -= Time.deltaTime * 2f; // Adjust fade speed here
            audioSource.volume = currentVolume;
            yield return null;
        }
        audioSource.Stop();
    }
}