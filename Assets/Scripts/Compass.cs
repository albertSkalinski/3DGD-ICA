using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Vector3 NorthDirection;
    public Transform Player;

    public RectTransform NorthLayer;

    void Update()
    {
        ChangeNorthDirection();
    }

    public void ChangeNorthDirection()
    {
        NorthDirection.z = Player.eulerAngles.y;
        NorthLayer.localEulerAngles = NorthDirection;
    }
}
