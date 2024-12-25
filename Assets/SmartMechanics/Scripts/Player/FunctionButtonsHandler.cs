using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class FunctionButtonsHandler : MonoBehaviour
{
    private InputManager _inputManager;
    private InputActionAsset _inputActions;
    private InputAction _inventoryAtion;
    private InputAction _pauseAction;

    private ViewUIManager _viewUIManager;
    private GameplayHandler _gameplayHandler;
    private AudioService _audioService;

    [Inject]
    private void Construct(ViewUIManager viewUIManager, InputManager inputManager, GameplayHandler gameplayHandler,
        AudioService audioService)
    {
        _viewUIManager = viewUIManager;
        _inputManager = inputManager;
        _inputActions = _inputManager.ActionAsset;
        _gameplayHandler = gameplayHandler;
        _audioService = audioService;
    }

    private void Start()
    {
        _inventoryAtion = _inputActions.FindAction("Inventory");
        _pauseAction = _inputActions.FindAction("Pause");

        _inventoryAtion.performed += context =>
        {
            if (context.performed)
            {
                var pausePage = _viewUIManager.GetView<PausePage>();
                var inventoryPage = _viewUIManager.GetView<MapInventoryPage>();
                var looseScreenPage = _viewUIManager.GetView<LoseScreenPage>();
                if (looseScreenPage.LoseScreen.activeInHierarchy || pausePage.PauseMenuPage.activeInHierarchy)
                {
                    return;
                }
                if (!inventoryPage.InventoryPage.activeInHierarchy)
                {
                    ShowAdditionalPage(inventoryPage);
                }
                else
                {
                    HideAdditionalPage(inventoryPage);
                }
            }
                
        };
        _pauseAction.performed += context =>
        {
            var pausePage = _viewUIManager.GetView<PausePage>();
            var inventoryPage = _viewUIManager.GetView<MapInventoryPage>();
            var looseScreenPage = _viewUIManager.GetView<LoseScreenPage>();
            if (looseScreenPage.LoseScreen.activeInHierarchy)
            {
                return;
            }
            if (!inventoryPage.InventoryPage.activeInHierarchy)
            {
                if (!pausePage.PauseMenuPage.activeInHierarchy)
                {
                    ShowAdditionalPage(pausePage);
                    Time.timeScale = 0;
                    _audioService.PauseGlobalAudio();
                }
                else
                {
                    HideAdditionalPage(pausePage);
                    Time.timeScale = 1;
                    _audioService.ContinueGlobalAudio();
                }
            }
            else
            {
                HideAdditionalPage(inventoryPage);
                ShowAdditionalPage(pausePage);
                Time.timeScale = 0;
                _audioService.PauseGlobalAudio();
            }
        };
    }

    private void ShowAdditionalPage(View view)
    {
        view.Show();
        _inputManager.DisableActionMap("Player");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideAdditionalPage(View view)
    {
        view.Hide();
        _inputManager.EnableActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
