using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadVirtualTour()
    {
        SceneManager.LoadScene("VirtualTour");
    }
}