using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class NavigationManager : MonoBehaviour
{
    public GameObject plantSelectionPanel;
    public GameObject draftPanel;
    public GameObject menuPanel;
    public GameObject enterDraftNamePopup;
    public TMP_InputField draftNameInputField;
    public GameObject draftsList;
    public Transform draftsListContainer;      // ScrollView-Content für die Entwürfe
    public GameObject draftListItemPrefab;     // UI-Element für eine einzelne Zeile
    public DraftManager draftManager;          // Referenz auf DraftManager

    public void closeAllPanels()
    {
        plantSelectionPanel.SetActive(false);
        draftPanel.SetActive(false);
        enterDraftNamePopup.SetActive(false);
        plantSelectionPanel.SetActive(false);
        draftsList.SetActive(false);
    }
    public void OnClick_Pflanzen()
    {
        closeAllPanels();
        // Panel mit Pflanzenliste ein-/ausblenden
        plantSelectionPanel.SetActive(!plantSelectionPanel.activeSelf);
    }

    public void OnClick_Entwurf()
    {
        closeAllPanels();
        // Panel mit Entwurfs-Funktionen (Speichern, Öffnen, etc.) ein-/ausblenden
        draftPanel.SetActive(!draftPanel.activeSelf);
    }

    public void OnClick_Menü()
    {
        closeAllPanels();
        // Hauptmenü oder Einstellungen öffnen
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    // Wird aufgerufen, wenn der Benutzer auf "Entwurf" → "Speichern" klickt
    public void OnClickSaveDialog()
    {
        enterDraftNamePopup.SetActive(!enterDraftNamePopup.activeSelf);
    }

    // Wird aufgerufen, wenn der Benutzer im Panel auf "Speichern bestätigen" klickt
    public void OnClickConfirmSaveDraft()
    {
        draftManager.SaveDraftToFile(draftNameInputField.text);
        OnClickSaveDialog();
        draftsList.SetActive(false);
    }

    public void ShowSavedDrafts()
    {
        closeAllPanels();
        // 1) Bestehende Listeneinträge löschen
        foreach (Transform child in draftsListContainer)
        {
            Destroy(child.gameObject);
        }

        // 2) Alle JSON-Dateien suchen
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json");
        foreach (var filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // 3) Prefab für die Listenzeile erzeugen
            GameObject listItem = Instantiate(draftListItemPrefab, draftsListContainer);

            // 4) DraftName beschriften
            TMP_Text textComp = listItem.transform.Find("DraftNameText").GetComponent<TMP_Text>();
            textComp.text = fileName;

            // 5) Button-Funktion zuweisen
            Button openButton = listItem.transform.Find("OpenButton").GetComponent<Button>();
            openButton.onClick.AddListener(() => OnClickOpenDraft(fileName));
        }

        draftsList.SetActive(true);
    }

    // Wird aufgerufen, wenn der Nutzer auf "Öffnen" in einer Zeile klickt
    private void OnClickOpenDraft(string draftName)
    {
        // Delegiere das Laden an den DraftManager
        draftManager.LoadDraftFromFile(draftName);
        closeAllPanels();
    }
}


