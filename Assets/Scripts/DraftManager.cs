using System;
using System.Collections.Generic;
using Google.XR.ARCoreExtensions.Samples.Geospatial;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


[System.Serializable]
public class Draft
{
    public string draftName;
    public List<GeospatialAnchorHistory> anchorHistories;

    public Draft(string draftName, List<GeospatialAnchorHistory> anchorHistories)
    {
        this.draftName = draftName;
        this.anchorHistories = anchorHistories;
    }
}

public class DraftJsonManager : MonoBehaviour
{
    // Beispielmethode, um ein Draft als JSON zu speichern
    public void SaveDraft(Draft draft)
    {
        // Dateiname basierend auf dem Draft-Namen
        string fileName = draft.draftName + ".json";

        // Verwende Application.persistentDataPath, um es auf dem Gerät zu speichern
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        // Konvertiere das Draft-Objekt in einen JSON-String
        string json = JsonUtility.ToJson(draft, prettyPrint: true);

        // Schreibe den JSON-String in die Datei
        File.WriteAllText(fullPath, json);

        Debug.Log($"Draft '{draft.draftName}' saved to: {fullPath}");
    }

    // Hier noch mit Lade-Skript erweitern:
    // public Draft LoadDraft(string draftName) { ... }
}
