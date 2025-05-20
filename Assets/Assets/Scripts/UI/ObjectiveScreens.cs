using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleUIScreen : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject endPanel;
    public GameManager gameManager;

    private PlayerInputActions inputActions;
    private bool gameStarted = false;

    private bool endScreenShown = false;
    private float endTimer = 0f;
    public float delayBeforeEndScreen = 10f;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.UI.StartGame.performed += OnStartGame;
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.UI.StartGame.performed -= OnStartGame;
        inputActions.Disable();
    }

    void Start()
    {
        Time.timeScale = 0f;
        startPanel.SetActive(true);
        endPanel.SetActive(false);
    }

    private void OnStartGame(InputAction.CallbackContext context)
    {
        if (gameStarted) return;

        gameStarted = true;
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (gameStarted && !endScreenShown && gameManager.AllOresScanned())
        {
            endTimer += Time.deltaTime;

            if (endTimer >= delayBeforeEndScreen)
            {
                endPanel.SetActive(true);
                endScreenShown = true;
            }
        }
    }
}