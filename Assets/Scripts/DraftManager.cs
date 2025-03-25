using System.Collections.Generic;
using Google.XR.ARCoreExtensions.Samples.Geospatial;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class DraftManager : MonoBehaviour
{
    public GeospatialController geoController;

    public void SaveDraftToFile(string draftName)
    {
        // Holt die aktuelle History vom Controller
        var historyCollection = geoController.GetAnchorHistory();

        // Sortieren, ggf. limitieren
        historyCollection.Collection.Sort((left, right) =>
            right.CreatedTime.CompareTo(left.CreatedTime));

        // Pfad und Dateiname
        string fileName = draftName + ".json";
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        // JSON
        string json = JsonUtility.ToJson(historyCollection);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Draft saved to: {fullPath}");
    }
    public void LoadDraftFromFile(string draftName)
    {
        // Aktuelle Szene leeren
        geoController.OnClearAllClicked();

        // Pfad ermitteln
        string filePath = Path.Combine(Application.persistentDataPath, draftName + ".json");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"Draft file {filePath} not found!");
            return;
        }

        // JSON einlesen
        string json = File.ReadAllText(filePath);
        GeospatialAnchorHistoryCollection loadedCollection =
            JsonUtility.FromJson<GeospatialAnchorHistoryCollection>(json);

        if (loadedCollection == null)
        {
            Debug.LogError("Failed to parse JSON or empty collection.");
            return;
        }

        //  _historyCollection im GeospatialController setzen
        geoController.SetHistoryCollection(loadedCollection);

        // ResolveHistory -> Anker platzieren
        geoController.ForceResolveHistory();

        Debug.Log($"Draft '{draftName}' loaded successfully!");
    }
}


