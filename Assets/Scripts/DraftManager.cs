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
    public TMP_InputField draftNameInputField;
    public void SaveDraftToFile()
    {
        // aktuelle History vom GeospatialController
        var historyCollection = geoController.GetAnchorHistory();

        // (Sortieren)
        historyCollection.Collection.Sort((left, right) =>
            right.CreatedTime.CompareTo(left.CreatedTime));

        // Pfad und Dateiname
        string draftName = draftNameInputField.text;
        string filePath = Path.Combine(Application.persistentDataPath, draftName + ".json");
        string json = JsonUtility.ToJson(historyCollection);

        // in JSON schreiben
        File.WriteAllText(filePath, json);
        Debug.Log($"Draft saved to: {filePath}");
    }
    public void LoadDraftFromFile(string draftName)
    {
        // Szene leeren
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

        //  _historyCollection im GeospatialController
        geoController.SetHistoryCollection(loadedCollection);

        // ResolveHistory -> Anker platzieren
        geoController.ForceResolveHistory();

        Debug.Log($"Draft '{draftName}' loaded successfully!");
    }
}


