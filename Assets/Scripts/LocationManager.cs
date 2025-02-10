using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    public Text locationInfoText;
    public float updateInterval = 2.0f;
    public bool locationReady = false;
    public bool useSimulation = true; // Falls kein echter Standort verfügbar ist

    private void Start()
    {
        Debug.Log("LocationManager: Starte Location Service.");
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("LocationManager: Location Services sind nicht aktiviert.");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            Debug.Log("LocationManager: Initialisierung...");
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait <= 0)
        {
            Debug.LogWarning("LocationManager: Zeitüberschreitung beim Initialisieren der Location Services.");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("LocationManager: Standortdaten konnten nicht abgerufen werden. Verwende Simulationsdaten.");
            useSimulation = true;
        }
        else
        {
            locationReady = true;
            Debug.Log("LocationManager: Location Services gestartet.");
        }
        StartCoroutine(UpdateLocationInfo());
    }

    IEnumerator UpdateLocationInfo()
    {
        while (true)
        {
            if (locationReady && Input.location.status == LocationServiceStatus.Running)
            {
                var loc = Input.location.lastData;
                string info = $"Lat: {loc.latitude:F5}\nLon: {loc.longitude:F5}\nAlt: {loc.altitude:F2}";
                if (locationInfoText != null)
                {
                    locationInfoText.text = info;
                }
                Debug.Log("LocationManager: " + info);
            }
            else if (useSimulation)
            {
                // Simulierte Standortdaten
                string info = "Simulierte Daten:\nLat: 52.5200\nLon: 13.4050\nAlt: 35.0";
                if (locationInfoText != null)
                {
                    locationInfoText.text = info;
                }
                Debug.Log("LocationManager (Sim): " + info);
            }
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
