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
        // Hole die aktuelle History vom Controller
        var historyCollection = geoController.GetAnchorHistory();

        // Sortieren, ggf. limitieren (wie im Original)
        historyCollection.Collection.Sort((left, right) =>
            right.CreatedTime.CompareTo(left.CreatedTime));
        // etc.

        // Pfad und Dateiname
        string draftname = draftNameInputField.text;
        string fileName = draftname + ".json";
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        // 4) JSON
        string json = JsonUtility.ToJson(historyCollection);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Draft saved to: {fullPath}");
    }
}

