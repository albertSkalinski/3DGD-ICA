using UnityEngine;
using UnityEngine.InputSystem;

public class OreScanner : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask oreLayer;
    public UIManager uiManager;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, oreLayer))
        {
            Ore ore = hit.collider.GetComponent<Ore>();
            if (ore != null)
            {
                uiManager.ShowScanPrompt();
                return;
            }
        }

        uiManager.HideScanPrompt();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, oreLayer))
        {
            Ore ore = hit.collider.GetComponent<Ore>();
            if (ore != null)
            {
                if (ore.Scan())
                {
                    GameManager.Instance.RegisterScan();
                }

                uiManager.ShowOrePopup(ore.oreData);
            }
        }
    }
}
