using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 10f;
    public float smoothSpeed = 5f; 
    public Vector2 boundaryX = new Vector2(-10f, 10f); 
    public Vector2 boundaryZ = new Vector2(-10f, 10f); 

    private Vector3 lastMousePosition;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            float moveX = delta.x * panSpeed * Time.deltaTime;
            float moveZ = delta.y * panSpeed * Time.deltaTime;

            
            targetPosition += new Vector3(moveX, 0, moveZ);
            targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryX.x, boundaryX.y);
            targetPosition.z = Mathf.Clamp(targetPosition.z, boundaryZ.x, boundaryZ.y);

            lastMousePosition = Input.mousePosition;
        }

        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
