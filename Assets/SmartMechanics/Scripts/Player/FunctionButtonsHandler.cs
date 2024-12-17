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

    [Inject]
    private void Construct(ViewUIManager viewUIManager, InputManager inputManager, GameplayHandler gameplayHandler)
    {
        _viewUIManager = viewUIManager;
        _inputManager = inputManager;
        _inputActions = _inputManager.ActionAsset;
        _gameplayHandler = gameplayHandler;
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
                }
                else
                {
                    HideAdditionalPage(pausePage);
                    Time.timeScale = 1;
                }
            }
            else
            {
                HideAdditionalPage(inventoryPage);
                ShowAdditionalPage(pausePage);
                Time.timeScale = 0;
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
