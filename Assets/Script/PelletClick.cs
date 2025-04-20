using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PeletObject pelet = hit.collider.GetComponent<PeletObject>();
                if (pelet != null)
                {
                    pelet.OnClick();
                }
            }
        }
    }
}
