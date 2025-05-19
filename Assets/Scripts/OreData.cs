using UnityEngine;

[CreateAssetMenu(fileName = "New Ore Data", menuName = "ScriptableObjects/OreData", order = 1)]
public class OreData : ScriptableObject
{
    public string oreName;
    [TextArea(3, 10)]
    public string description;
}
