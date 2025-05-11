using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public LayerMask hoverMask;
    private GameObject lastHovered;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hoverMask))
        {
            if (hit.collider.gameObject != lastHovered)
            {
                if (lastHovered != null)
                    lastHovered.layer = LayerMask.NameToLayer("Default");

                lastHovered = hit.collider.gameObject;
                lastHovered.layer = LayerMask.NameToLayer("OutlineObject");
            }
        }
        else
        {
            if (lastHovered != null)
                lastHovered.layer = LayerMask.NameToLayer("Default");
        }
    }
}
