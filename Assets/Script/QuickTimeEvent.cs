using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public Slider qteSlider;
    public float fishBasePullSpeed = 0.2f;
    public float fishPullIncreaseOverTime = 0.05f;
    public float playerPullStrength = 0.1f;
    public float targetMin = 0.1f;
    public float targetMax = 1f;
    public GameObject qtePanel;
    public GameObject qteRing;
    public FishingEvent fishingEvent;

    private bool isQteActive = false;
    private FishData currentFish;
    private float qteTimer = 0f;

    void Start()
    {
        qtePanel.SetActive(false);
        qteSlider.value = 0.5f;
    }

    void Update()
    {
        if (!isQteActive) return;

        qteTimer += Time.deltaTime;


        float currentFishPull = fishBasePullSpeed + (qteTimer * fishPullIncreaseOverTime);
        float fishForce = -currentFishPull * Time.deltaTime;


        float playerForce = 0f;
        if (Input.mouseScrollDelta.y > 0)
        {

            float resistance = Mathf.Pow(1f - qteSlider.value, 2f);
            playerForce = playerPullStrength * resistance;
        }

        float totalForce = fishForce + playerForce;
        qteSlider.value = Mathf.Clamp01(qteSlider.value + totalForce);


        if (Input.GetMouseButtonDown(0))
        {
            if (qteSlider.value >= targetMin && qteSlider.value <= targetMax)
            {
                Debug.Log("QTE SUCCESS!");
                EndQTE(true);
            }
            else
            {
                Debug.Log("QTE FAILED!");
                EndQTE(false);
            }
        }
    }

    public void StartQTE(FishData fish)
    {
        currentFish = fish;
        isQteActive = true;
        qtePanel.SetActive(true);
        qteSlider.value = 0.5f;
        qteTimer = 0f;
    }

    private void EndQTE(bool success)
    {
        isQteActive = false;
        qtePanel.SetActive(false);
        fishingEvent.FinishFishing(success, currentFish);
    }
}
