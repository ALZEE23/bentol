using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    private int defaultLayer;
    public int outlineLayer = 10;
    private Transform[] allChildren;

    private void Start()
    {
        defaultLayer = gameObject.layer;
        allChildren = GetComponentsInChildren<Transform>(true);
    }

    private void OnMouseEnter()
    {
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = outlineLayer;
        }
    }

    private void OnMouseExit()
    {
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = defaultLayer;
        }
    }
}
