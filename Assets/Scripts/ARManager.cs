using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [Header("AR Foundation Components")]
    public ARSession arSession;
    public ARPlaneManager arPlaneManager;
    public ARAnchorManager arAnchorManager;

    public static ARManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        Debug.Log("ARManager wurde initialisiert.");
        // Prüfe auch, ob alle Komponenten gesetzt sind:
        if(arPlaneManager == null)
            Debug.LogError("ARPlaneManager ist nicht zugewiesen!");
        if(arSession == null)
            Debug.LogError("ARSession ist nicht zugewiesen!");
        if(arAnchorManager == null)
            Debug.LogError("ARAnchorManager ist nicht zugewiesen!");
    }

    private void Start()
    {
        if (arSession != null)
        {
            Debug.Log("Starte AR Session.");
            arSession.Reset();
        }
    }
}
