using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InteractionController : MonoBehaviour
{
    [Header("Placement Object")]
    public GameObject objectPrefab;

    private bool objectPlaced = false;
    private FacadeDetector facadeDetector;

    private void Awake()
    {
        facadeDetector = GetComponent<FacadeDetector>();
        if (facadeDetector == null)
        {
            Debug.LogError("InteractionController: FacadeDetector component fehlt!");
        }
        else
        {
            Debug.Log("InteractionController: FacadeDetector gefunden.");
        }
    }

    private void OnEnable()
    {
        if (facadeDetector != null)
        {
            facadeDetector.OnFacadeDetected += PlaceObjectOnFacade;
            Debug.Log("InteractionController: Callback für Fassade registriert.");
        }
    }

    private void OnDisable()
    {
        if (facadeDetector != null)
        {
            facadeDetector.OnFacadeDetected -= PlaceObjectOnFacade;
            Debug.Log("InteractionController: Callback für Fassade abgemeldet.");
        }
    }

    private void PlaceObjectOnFacade(ARPlane facadePlane)
    {
        if (objectPlaced)
            return;

        Debug.Log("InteractionController: Platziere 3D-Objekt an der erkannten Fassade.");
        Vector3 placementPos = facadeDetector.GetPlacementPosition(facadePlane);
        Quaternion placementRot = facadePlane.transform.rotation;

        // Erstelle einen ARAnchor mittels AttachAnchor (wird an der Fassade angehängt)
        ARAnchor localAnchor = ARManager.Instance.arAnchorManager.AttachAnchor(facadePlane, new Pose(placementPos, placementRot));
        if (localAnchor == null)
        {
            Debug.LogError("InteractionController: Lokaler ARAnchor konnte nicht erstellt werden.");
            return;
        }
        Debug.Log("InteractionController: ARAnchor erfolgreich erstellt.");

        // Instanziiere das 3D-Objekt als Kind des Ankers.
        GameObject placedObj = Instantiate(objectPrefab, localAnchor.transform);
        Debug.Log($"InteractionController: 3D-Objekt instanziiert an Position: {placedObj.transform.position}");

        objectPlaced = true;
    }
}
