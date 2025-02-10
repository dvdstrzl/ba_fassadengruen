using UnityEngine;
using System.IO;

[System.Serializable]
public class DesignSnapshot
{
    public Vector3 worldPosition;
    public Quaternion worldRotation;
    public float latitude;
    public float longitude;
    public float altitude;
    // Optional: Cloud Anchor ID, falls später integriert
    public string cloudAnchorId;
}

public static class DesignSnapshotRepository
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "snapshot.json");

    public static void SaveSnapshot(DesignSnapshot snapshot)
    {
        string json = JsonUtility.ToJson(snapshot, true);
        File.WriteAllText(filePath, json);
        Debug.Log("DesignSnapshotRepository: Snapshot gespeichert unter " + filePath);
    }

    public static DesignSnapshot LoadSnapshot()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("DesignSnapshotRepository: Kein Snapshot gefunden.");
            return null;
        }
        string json = File.ReadAllText(filePath);
        DesignSnapshot snapshot = JsonUtility.FromJson<DesignSnapshot>(json);
        Debug.Log("DesignSnapshotRepository: Snapshot geladen.");
        return snapshot;
    }
}
