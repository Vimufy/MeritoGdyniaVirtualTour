using UnityEngine;

public class VirtualTour : MonoBehaviour
{
    public Material SkyMaterial;
    public string currentSpotNumber = "1";

    private void Start()
    {
        // Kopia materiału, żeby w czasie gry nie zmieniać pliku w projekcie
        SkyMaterial = new Material(SkyMaterial);
        RenderSettings.skybox = SkyMaterial;
        ShowSpot(currentSpotNumber);
    }

    public void ShowSpot(string spotNumber)
    {
        currentSpotNumber = spotNumber;
        Texture2D skyTexture = Resources.Load<Texture2D>("Panoramas/img (" + currentSpotNumber + ")");
        SkyMaterial.SetTexture("_MainTex", skyTexture);
    }
}
