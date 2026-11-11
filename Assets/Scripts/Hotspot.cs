using UnityEngine;
using TMPro;

public class Hotspot : MonoBehaviour
{
    public string target;
    public TMP_Text label;

    void Start()
    {
        if (label != null) label.text = target;
    }

    void LateUpdate()
    {
        if (label != null) label.transform.rotation = Camera.main.transform.rotation;
    }

    public void Click()
    {
        FindAnyObjectByType<VirtualTour>().ShowSpot(target);
    }
}