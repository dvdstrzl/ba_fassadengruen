using System.Collections.Generic;
using Google.XR.ARCoreExtensions.Samples.Geospatial;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class DraftManager : MonoBehaviour
{
    public GeospatialController geoController;
    // Referenz auf das InputField, wo der Benutzer den Namen eingibt

    public void SaveDraftToFile(string draftName)
    {
        // Hole die aktuelle History vom Controller
        var historyCollection = geoController.GetAnchorHistory();

        // Sortieren, ggf. limitieren (wie im Original)
        historyCollection.Collection.Sort((left, right) =>
            right.CreatedTime.CompareTo(left.CreatedTime));
        // etc.

        // Pfad und Dateiname
        string fileName = draftName + ".json";
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        // 4) JSON
        string json = JsonUtility.ToJson(historyCollection);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Draft saved to: {fullPath}");
    }
    public void LoadDraftFromFile(string draftName)
    {
        // 1) Szene leeren
        geoController.OnClearAllClicked();

        // 2) Pfad ermitteln
        string filePath = Path.Combine(Application.persistentDataPath, draftName + ".json");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"Draft file {filePath} not found!");
            return;
        }

        // 3) JSON einlesen
        string json = File.ReadAllText(filePath);
        GeospatialAnchorHistoryCollection loadedCollection =
            JsonUtility.FromJson<GeospatialAnchorHistoryCollection>(json);

        if (loadedCollection == null)
        {
            Debug.LogError("Failed to parse JSON or empty collection.");
            return;
        }

        // 4) Setze das _historyCollection im GeospatialController
        geoController.SetHistoryCollection(loadedCollection);

        // 5) ResolveHistory -> Anker platzieren
        geoController.ForceResolveHistory();

        Debug.Log($"Draft '{draftName}' loaded successfully!");
    }
}


