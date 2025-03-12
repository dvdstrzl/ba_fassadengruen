using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlantSelectionManager : MonoBehaviour
{
    public GameObject[] plantPrefabs; // Array der verschiedenen Pflanzen-Prefabs
    public ARStreetscapeGeometryManager streetscapeGeometryManager; // Referenz zum ARStreetscapeGeometryManager
    public GameObject currentPlantPrefab; // Aktuell ausgewähltes Pflanzen-Prefab

    void Start()
    {
        // ARStreetscapeGeometryManager soll nur einen nackten ARAnchor erzeugen
        streetscapeGeometryManager.AnchorPrefab = null;

        // if (plantPrefabs != null && plantPrefabs.Length > 0)
        // {
        //     currentPlantPrefab = plantPrefabs[0];
        //     // Falls ARStreetscapeGeometryManager.AnchorPrefab hier auch updaten soll:
        //     // UpdateAnchorPrefab();
        // }
        // else
        // {
        //     Debug.LogError("Keine Pflanzen-Prefabs zugewiesen!");
        // }
    }

    // Funktion, die von den UI-Buttons aufgerufen wird, um die Pflanze auszuwählen
    public void SelectPlant(int plantIndex)
    {
        if (plantIndex >= 0 && plantIndex < plantPrefabs.Length)
        {
            currentPlantPrefab = plantPrefabs[plantIndex];
            UpdateAnchorPrefab();
            Debug.Log("Ausgewählte Pflanze: " + currentPlantPrefab.name);
        }
        else
        {
            Debug.LogError("Ungültiger Pflanzenobjekt-Index!");
        }
    }

    // Helpermethode (für PlaceAnchorByScreenTap)
    public GameObject GetPrefabByName(string prefabName)
    {
        foreach (var prefab in plantPrefabs)
        {
            if (prefab.name == prefabName)
                return prefab;
        }
        return null;
    }

    // Aktualisiert das AnchorPrefab im ARStreetscapeGeometryManager
    private void UpdateAnchorPrefab()
    {
        if (streetscapeGeometryManager != null && currentPlantPrefab != null)
        {
            streetscapeGeometryManager.AnchorPrefab = currentPlantPrefab;
            Debug.Log("AnchorPrefab aktualisiert auf: " + currentPlantPrefab.name);
            Debug.Log("streetscapeGeometryManager.AnchorPrefab:" + streetscapeGeometryManager.AnchorPrefab.name);

        }
    }
}
