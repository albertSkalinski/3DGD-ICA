using UnityEngine;

public class Ore : MonoBehaviour
{
    public OreData oreData;
    public bool isScanned = false;

    public bool Scan()
    {
        if (isScanned)
            return false;

        isScanned = true;
        return true;
    }
}