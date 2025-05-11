using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OutlineOnHover : MonoBehaviour
{
    private Material originalMat;
    public Material outlineMaterial;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = rend.material;
    }

    void OnMouseEnter()
    {
        rend.material = outlineMaterial;
    }

    void OnMouseExit()
    {
        rend.material = originalMat;
    }
}
