using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    private int defaultLayer;
    public int outlineLayer = 10; // pastikan kamu buat layer ini di Project Settings > Tags and Layers

    private void Start()
    {
        defaultLayer = gameObject.layer;
    }

    private void OnMouseEnter()
    {
        gameObject.layer = outlineLayer;
    }

    private void OnMouseExit()
    {
        gameObject.layer = defaultLayer;
    }
}
