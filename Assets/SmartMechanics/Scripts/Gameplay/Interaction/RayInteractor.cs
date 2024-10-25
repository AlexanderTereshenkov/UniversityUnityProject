using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayInteractor : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float maxDistance;
    [SerializeField] private Inventory inventory;

    private InteractionTextUI _interactionTextUI;

    private InputActionAsset _actionAsset;
    private InputAction _interactAction;
    private IInteractible _currentInteractible;
    private ViewUIManager _viewUIManager;

    [Inject]
    private void Construct(ViewUIManager viewUIManager, InputActionAsset inputAction)
    {
        _viewUIManager = viewUIManager;
        _actionAsset = inputAction;
    }

    private void Start()
    {
        _interactAction = _actionAsset.FindAction("Interact");
        _interactAction.performed += Interact;
        _interactionTextUI = _viewUIManager.GetView<InteractionTextUI>();
        layerMask = ~layerMask;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(rayOrigin.position,rayOrigin.forward * maxDistance, Color.red);
        if(Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, maxDistance, layerMask))
        {
            if(hit.collider.TryGetComponent(out IInteractible interactible))
            {
                _currentInteractible = interactible;
                _interactionTextUI.SetText(_currentInteractible.GetStringDescription());
            }
            else
            {
                _interactionTextUI.SetText(string.Empty);
                _currentInteractible = null;
            }
        }
        else
        {
            _interactionTextUI.SetText(string.Empty);
            _currentInteractible = null;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(_currentInteractible != null)
            {
                _currentInteractible.Interact(inventory);
            }
        }
    }

}
