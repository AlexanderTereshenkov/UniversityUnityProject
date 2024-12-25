using Reflex.Attributes;
using Trisibo;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenPage : View
{
    [SerializeField] private GameObject looseScreenPage;
    [SerializeField] private SceneField mainMenu;

    [SerializeField] private Button restartCheckpoint;
    [SerializeField] private Button restartGame;
    [SerializeField] private Button exitToMenu;

    private GameplayHandler _gameplayHandler;
    private SceneLoader _sceneLoader;

    public GameObject LoseScreen
    {
        get
        {
            return looseScreenPage;
        }
    }

    [Inject]
    private void Construct(GameplayHandler gameplayHandler, SceneLoader sceneLoader)
    {
        _gameplayHandler = gameplayHandler;
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        _viewUIManager.RegisterView(this);
        restartCheckpoint.onClick.AddListener(
            () =>
            {
                _gameplayHandler.RestartGame(true);
            }
            );
        restartGame.onClick.AddListener(
            () =>
            {
                _gameplayHandler.RestartGame(false);
            });
        exitToMenu.onClick.AddListener(() => { StartCoroutine(_sceneLoader.LoadScene(mainMenu.BuildIndex)); });
    }

    public override void Hide()
    {
        looseScreenPage.SetActive(false);
        _viewUIManager.ShowView(ViewType.Gameplay);
    }

    public override void Show()
    {
        _viewUIManager.HideAllViews();
        looseScreenPage.SetActive(true);
    }
}
