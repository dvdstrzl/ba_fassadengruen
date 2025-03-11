using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    public GameObject plantSelectionPanel;
    public GameObject draftPanel;
    public GameObject menuPanel;
    public GameObject EnterDraftNamePopup;
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

    // Wird aufgerufen, wenn der Benutzer auf "Entwurf" → "Speichern" klickt
    public void OnClickSaveDialog()
    {
        EnterDraftNamePopup.SetActive(!EnterDraftNamePopup.activeSelf);
    }

    // Wird aufgerufen, wenn der Benutzer im Panel auf "Speichern bestätigen" klickt
    public void OnClickConfirmSave()
    {
        OnClickSaveDialog();
        draftPanel.SetActive(false);
    }
}


