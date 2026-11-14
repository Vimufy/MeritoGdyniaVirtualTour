using UnityEngine;
using TMPro;

public class Hotspot : MonoBehaviour
{
    public string target;

    public void Click()
    {
        FindAnyObjectByType<VirtualTour>().ShowSpot(target);
    }
}