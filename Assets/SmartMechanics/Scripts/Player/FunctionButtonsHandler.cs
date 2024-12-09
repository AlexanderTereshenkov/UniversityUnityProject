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

    [Inject]
    private void Construct(ViewUIManager viewUIManager, InputManager inputManager)
    {
        _viewUIManager = viewUIManager;
        _inputManager = inputManager;
        _inputActions = _inputManager.ActionAsset;
    }

    private void Start()
    {
        _inventoryAtion = _inputActions.FindAction("Inventory");
        _pauseAction = _inputActions.FindAction("Pause");

        _inventoryAtion.performed += context =>
        {
            if (context.performed)
            {
                var inventoryPage = _viewUIManager.GetView<MapInventoryPage>();
                if (!inventoryPage.InventoryPage.activeInHierarchy)
                {
                    inventoryPage.Show();
                    _inputManager.DisableActionMap("Player");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    inventoryPage.Hide();
                    _inputManager.EnableActionMap("Player");
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
                
        };

    }

}
