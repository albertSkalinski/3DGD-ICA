using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI oreTitle;
    public TextMeshProUGUI oreDescription;
    public GameObject scanPrompt;

    public void ShowOrePopup(OreData oreData)
    {
        popupPanel.SetActive(true);
        oreTitle.text = oreData.oreName;
        oreDescription.text = oreData.description;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HidePopup()
    {
        Debug.Log("Close button clicked");
        popupPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowScanPrompt()
    {
        scanPrompt.SetActive(true);
    }

    public void HideScanPrompt()
    {
        scanPrompt.SetActive(false);
    }
}