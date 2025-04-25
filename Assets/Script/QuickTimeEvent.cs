using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public Slider qteSlider;
    public float scrollSpeed = 0.1f;
    public float targetMin = 0.4f;
    public float targetMax = 0.6f;
    public GameObject qtePanel;
    public FishingEvent fishingEvent;
    public float oscillationSpeed = 3f; 
    public float oscillationAmount = 1f; 
    public float stabilizeSpeed = 0.5f; 

    private bool isQteActive = false;
    private FishData currentFish; 
    private float time;
    private float targetValue = 0.5f;  

    void Start()
    {
        qtePanel.SetActive(false);
        qteSlider.value = 0.5f;
    }

    void Update()
    {
        if (!isQteActive) return;

        
        time += Time.deltaTime * oscillationSpeed;
        float oscillation = Mathf.Sin(time) * oscillationAmount;
        float autoMove = 0.5f + oscillation * 0.5f;
        
        
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        targetValue = Mathf.Clamp01(targetValue + mouseWheel * stabilizeSpeed);
        
        
        qteSlider.value = Mathf.Lerp(autoMove, targetValue, 0.5f);

        if (Input.GetMouseButtonDown(0))
        {
            if (qteSlider.value >= targetMin && qteSlider.value <= targetMax)
            {
                Debug.Log("QTE SUCCESS!");
                isQteActive = false;
                qtePanel.SetActive(false);
                fishingEvent.FinishFishing(true, currentFish);
            }
            else
            {
                Debug.Log("QTE FAILED!");
                isQteActive = false;
                qtePanel.SetActive(false);
                fishingEvent.FinishFishing(false, currentFish);
            }
        }
    }

    public void StartQTE(FishData fish)
    {
        currentFish = fish;
        isQteActive = true;
        qtePanel.SetActive(true);
        qteSlider.value = 0.5f;
        time = 0f; 
    }
}

