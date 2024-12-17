using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenPage : View
{
    [SerializeField] private GameObject looseScreenPage;

    [SerializeField] private Button restartCheckpoint;
    [SerializeField] private Button restartGame;
    [SerializeField] private Button exitToMenu;

    private GameplayHandler _gameplayHandler;

    public GameObject LoseScreen
    {
        get
        {
            return looseScreenPage;
        }
    }

    [Inject]
    private void Construct(GameplayHandler gameplayHandler)
    {
        _gameplayHandler = gameplayHandler;
    }

    private void Start()
    {
        _viewUIManager.RegisterView(this);
        restartGame.onClick.AddListener(
            () =>
            {
                _gameplayHandler.RestartGame();
            });
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
