using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkanController : MonoBehaviour
{
    public GameObject ikan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFish(){
        ikan.SetActive(true);
    }

    public void OffFish(){
        ikan.SetActive(false);
    }
}
