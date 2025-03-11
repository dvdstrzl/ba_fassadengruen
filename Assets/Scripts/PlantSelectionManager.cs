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
        // Stelle sicher, dass das erste Pflanzen-Prefab standardmäßig ausgewählt ist
        if (plantPrefabs != null && plantPrefabs.Length > 0)
        {
            currentPlantPrefab = plantPrefabs[0];
            UpdateAnchorPrefab();
        }
        else
        {
            Debug.LogError("Keine Pflanzen-Prefabs zugewiesen!");
        }
    }

    // Funktion, die von den UI-Buttons aufgerufen wird, um die Pflanze auszuwählen
    public void SelectPlant(int plantIndex)
    {
        if (plantIndex >= 0 && plantIndex < plantPrefabs.Length)
        {
            currentPlantPrefab = plantPrefabs[plantIndex];
            UpdateAnchorPrefab();
        }
        else
        {
            Debug.LogError("Ungültiger Pflanzenindex!");
        }
    }

    // Aktualisiert das AnchorPrefab im ARStreetscapeGeometryManager
    private void UpdateAnchorPrefab()
    {
        if (streetscapeGeometryManager != null)
        {
            streetscapeGeometryManager.AnchorPrefab = currentPlantPrefab;
            Debug.Log("Anchor Prefab aktualisiert auf: " + currentPlantPrefab.name);
        }
        else
        {
            Debug.LogError("ARStreetscapeGeometryManager nicht zugewiesen!");
        }
    }
}
