using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Connection
{
    public string point;
    public float angle;
    public string neighbor;
}

public class VirtualTour : MonoBehaviour
{
    public Material skyMaterial;
    public GameObject hotspotPrefab;
    public string currentSpotNumber = "1";
    public List<Connection> connections = new List<Connection>();

    private List<GameObject> spawnedHotspots = new List<GameObject>();

    private void Start()
    {
        skyMaterial = new Material(skyMaterial);
        RenderSettings.skybox = skyMaterial;
        ShowSpot(currentSpotNumber);
    }

    public void ShowSpot(string spotNumber)
    {
        currentSpotNumber = spotNumber;
        Texture2D skyTexture = Resources.Load<Texture2D>("Panoramas/img (" + spotNumber + ")");
        skyMaterial.SetTexture("_MainTex", skyTexture);

        foreach (GameObject hotspot in spawnedHotspots)
        {
            Destroy(hotspot);
        }
        spawnedHotspots.Clear();

        foreach (Connection connection in connections)
        {
            if (connection.point != spotNumber) continue;
            Vector3 direction = Quaternion.Euler(0f, connection.angle + 90f, 0f) * Vector3.forward;
            Vector3 position = direction * 4f + Vector3.down * 1.5f;
            GameObject newHotspot = Instantiate(hotspotPrefab, position, Quaternion.identity);
            newHotspot.GetComponent<Hotspot>().target = connection.neighbor;
            spawnedHotspots.Add(newHotspot);
        }
    }
}