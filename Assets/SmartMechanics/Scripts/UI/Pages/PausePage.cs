using Reflex.Attributes;
using Trisibo;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : View
{
    [SerializeField] private GameObject pausePage;
    [SerializeField] private SceneField mainMenu;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    private GameplayHandler _gameplayHandler;
    private InputManager _inputManager;
    private SceneLoader _sceneLoader;
    private AudioService _audioService;

    public GameObject PauseMenuPage
    {
        get
        {
            return pausePage;
        }
    }

    [Inject]
    private void Construct(GameplayHandler gameplayHandler, InputManager inputManager, SceneLoader sceneLoader,
        AudioService audioService)
    {
        _gameplayHandler = gameplayHandler;
        _inputManager = inputManager;
        _sceneLoader = sceneLoader;
        _audioService = audioService;
    }

    private void Start()
    {
        _viewUIManager.RegisterView(this);
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitToMenu);
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
        _audioService.ContinueGlobalAudio();
    }

    private void ExitToMenu()
    {
        _sceneLoader.LoadScene(mainMenu.BuildIndex);
    }

}
