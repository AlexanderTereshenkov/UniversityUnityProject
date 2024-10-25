using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class FunctionButtonsHandler : MonoBehaviour
{
    private InputActionAsset _inputActions;
    private InputAction _inventoryAtion;
    private InputAction _pauseAction;

    private ViewUIManager _viewUIManager;

    [Inject]
    private void Construct(InputActionAsset inputActions, ViewUIManager viewUIManager)
    {
        _inputActions = inputActions;
        _viewUIManager = viewUIManager;
    }

    private void Start()
    {
        _inventoryAtion =  _inputActions.FindAction("Inventory");
        _pauseAction = _inputActions.FindAction("Pause");

        _inventoryAtion.performed += context =>
        {
            if (context.performed)
            {
                var inventoryPage = _viewUIManager.GetView<MapInventoryPage>();
                if (!inventoryPage.InventoryPage.activeInHierarchy)
                {
                    inventoryPage.Show();
                    _inputActions.FindActionMap("Player").Disable();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    inventoryPage.Hide();
                    _inputActions.FindActionMap("Player").Enable();
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
                
        };

    }

}
