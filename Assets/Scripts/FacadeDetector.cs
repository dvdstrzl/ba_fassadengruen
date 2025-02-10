using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FacadeDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    public float minWidth = 2.0f;
    public float minHeight = 2.0f;
    public float verticalOffset = 2.0f;

    public Action<ARPlane> OnFacadeDetected;

    private ARPlaneManager planeManager;
    private bool facadeFound = false;

    private void Awake()
    {
        if (ARManager.Instance == null)
        {
            Debug.LogError("ARManager.Instance ist null! Stelle sicher, dass ein GameObject mit ARManager in der Szene vorhanden und aktiv ist.");
            return;
        }

        planeManager = ARManager.Instance.arPlaneManager;
        if (planeManager == null)
        {
            Debug.LogError("ARManager.Instance.arPlaneManager ist null! Weisen Sie im Inspector dem ARManager eine gültige ARPlaneManager-Komponente zu.");
        }
        else
        {
            Debug.Log("FacadeDetector: ARPlaneManager erfolgreich referenziert.");
        }
    }

    private void Update()
    {
        if (facadeFound || planeManager == null)
            return;

        foreach (ARPlane plane in planeManager.trackables)
        {
            Debug.Log($"Prüfe Plane: Alignment={plane.alignment}, Größe=({plane.size.x:F2}, {plane.size.y:F2})");
            if (plane.alignment == PlaneAlignment.Vertical &&
                plane.size.x >= minWidth && plane.size.y >= minHeight)
            {
                Debug.Log("Geeignete Fassade erkannt!");
                OnFacadeDetected?.Invoke(plane);
                facadeFound = true;
                break;
            }
        }
    }

    public Vector3 GetPlacementPosition(ARPlane plane)
    {
        Vector3 localPos = plane.center + new Vector3(0, verticalOffset, 0);
        Vector3 worldPos = plane.transform.TransformPoint(localPos);
        Debug.Log($"Berechnete Platzierungsposition: {worldPos}");
        return worldPos;
    }
}
