using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image darkening;

    private bool isFirstLoad = true;

    private void Start()
    {
        skyMaterial = new Material(skyMaterial);
        RenderSettings.skybox = skyMaterial;
        
        ShowSpot(currentSpotNumber);
    }

    public void ShowSpot(string spotNumber)
    {
        StartCoroutine(ChangeSpotRoutine(spotNumber));
    }

    private IEnumerator ChangeSpotRoutine(string spotNumber)
    {
        if (!isFirstLoad)
        {
            yield return StartCoroutine(FadeScreen(0f, 1f));
        }
        else
        {
            darkening.color = new Color(0f, 0f, 0f, 0f);
        }

        currentSpotNumber = spotNumber;
        Texture2D skyTexture = Resources.Load<Texture2D>("Panoramas/" + spotNumber);
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
            
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            
            GameObject newHotspot = Instantiate(hotspotPrefab, position, rotation);
            newHotspot.GetComponent<Hotspot>().target = connection.neighbor;
            spawnedHotspots.Add(newHotspot);
        }

        if (isFirstLoad)
        {
            isFirstLoad = false;
        }
        else
        {
            yield return StartCoroutine(FadeScreen(1f, 0f));
        }
    }

    private IEnumerator FadeScreen(float fromValue, float toValue)
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / 0.3f)
        {
            darkening.color = new Color(0f, 0f, 0f, Mathf.Lerp(fromValue, toValue, t));
            yield return null;
        }
        darkening.color = new Color(0f, 0f, 0f, toValue);
    }
}