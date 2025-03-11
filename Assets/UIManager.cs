using UnityEngine;
using UnityEngine.UI;
using Google.XR.ARCoreExtensions.Samples.Geospatial;

public class NavigationManager : MonoBehaviour
{
    public GameObject plantSelectionPanel;
    public GameObject draftPanel;
    public GameObject menuPanel;



    public void OnClick_Pflanzen()
    {
        // Panel mit Pflanzenliste ein-/ausblenden
        plantSelectionPanel.SetActive(!plantSelectionPanel.activeSelf);
    }

    public void OnClick_Entwurf()
    {
        // Panel mit Entwurfs-Funktionen (Speichern, Öffnen, etc.) ein-/ausblenden
        draftPanel.SetActive(!draftPanel.activeSelf);
    }

    public void OnClick_Menü()
    {
        // Hauptmenü oder Einstellungen öffnen
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public class DraftUIManager : MonoBehaviour
    {
        public GameObject saveDialogPanel;
        public InputField draftNameInputField;
        public DraftJsonManager draftJsonManager;
        public GeospatialController arController; // Referenz, um die platzierten Anker abzurufen

        // Wird aufgerufen, wenn der Benutzer auf "Entwurf" → "Speichern" klickt
        public void OnClickShowSaveDialog()
        {
            saveDialogPanel.SetActive(true);
        }

        // Wird aufgerufen, wenn der Benutzer im Panel auf "Speichern bestätigen" klickt
        public void OnClickConfirmSave()
        {
            // 1) Den Namen aus dem Eingabefeld abrufen
            string nameEntered = draftNameInputField.text;

            // 2) Die aktuelle Ankerliste aus AR abrufen
            var anchors = arController.placedAnchors;

            // 3) Ein neues Entwurfsobjekt erstellen
            Draft newDraft = new Draft(nameEntered, anchors);

            // 4) Den Entwurf mithilfe des DraftJsonManager speichern
            draftJsonManager.SaveDraft(newDraft);

            // 5) Das Panel ausblenden
            saveDialogPanel.SetActive(false);
        }
    }


}
