using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformGun : MonoBehaviour
{
    [Header("Gun settings")]
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float gunDistance;
    [SerializeField] private LayerMask gunLayer;
    [Header("Follow point")]
    [SerializeField] private Transform followPoint;

    private InputActionAsset _inputActionAsset;
    private InputAction _shootAction;
    private bool _isGrabbed;
    private TransformBlock _currentBlock;

    [Inject]
    private void Construct(InputActionAsset inputAction)
    {
        _inputActionAsset = inputAction;
    }

    private void Start()
    {
        _shootAction = _inputActionAsset.FindAction("Shoot");
        _shootAction.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BlockInteraction();
        }
    }

    private void BlockInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, gunDistance, gunLayer))
        {
            if (!_isGrabbed)
            {
                if (hit.collider.TryGetComponent(out TransformBlock transformBlock))
                {
                    transformBlock.GrabObject(followPoint);
                    _currentBlock = transformBlock;
                    _isGrabbed = true;
                }
            }
            else
            {
                if(hit.collider.TryGetComponent(out TransformBlockPlace transformBlockPlace))
                {
                    _currentBlock.ReleaseObject(hit.transform);
                    _currentBlock = null;
                    _isGrabbed = false;
                }
            }
            
        }
    }

}
