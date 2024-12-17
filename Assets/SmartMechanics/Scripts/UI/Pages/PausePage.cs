using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : View
{
    [SerializeField] private GameObject pausePage;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    private GameplayHandler _gameplayHandler;
    private InputManager _inputManager;

    public GameObject PauseMenuPage
    {
        get
        {
            return pausePage;
        }
    }

    [Inject]
    private void Construct(GameplayHandler gameplayHandler, InputManager inputManager)
    {
        _gameplayHandler = gameplayHandler;
        _inputManager = inputManager;
    }

    private void Start()
    {
        _viewUIManager.RegisterView(this);
        continueButton.onClick.AddListener(ContinueGame);
    }
    public override void Hide()
    {
        pausePage.SetActive(false);
        _viewUIManager.ShowView(ViewType.Gameplay);
    }

    public override void Show()
    {
        _viewUIManager.HideAllViews();
        pausePage.SetActive(true);
    }

    private void ContinueGame()
    {
        Hide();
        Time.timeScale = 1;
        _gameplayHandler.ShowHideCursor(false);
        _inputManager.EnableActionMap("Player");
    }

}
