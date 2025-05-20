using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalOres;
    public int scannedOres;

    public UIManager uiManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalOres = FindObjectsOfType<Ore>().Length;
        scannedOres = 0;
        uiManager.UpdateScanProgress(scannedOres, totalOres);
    }

    public void RegisterScan()
    {
        scannedOres++;
        uiManager.UpdateScanProgress(scannedOres, totalOres);
    }
    public bool AllOresScanned()
    {
        return scannedOres >= totalOres;
    }
}