using Reflex.Attributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class TransformGun : MonoBehaviour, IRestartable
{
    [Header("Gun settings")]
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Transform gunTipPoint;
    [SerializeField] private float gunDistance;
    [SerializeField] private LayerMask gunLayer;
    [Header("Follow point")]
    [SerializeField] private Transform followPoint;

    private InputActionAsset _inputActionAsset;
    private InputAction _shootAction;
    private bool _isGrabbed;
    private TransformBlock _currentBlock;
    private RespawnManager _respawnManager;
    //private LineRenderer _lineRenderer;

    [Inject]
    private void Construct(InputManager inputManager, RespawnManager respawnManager)
    {
        _inputActionAsset = inputManager.ActionAsset;
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        _shootAction = _inputActionAsset.FindAction("Shoot");
        _shootAction.performed += Shoot;
        //_lineRenderer = GetComponent<LineRenderer>();
        _respawnManager.Register(this);
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

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, gunDistance))
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
                if (hit.collider.TryGetComponent(out TransformBlockPlace transformBlockPlace))
                {
                    transformBlockPlace.SetBlock(_currentBlock);
                    _currentBlock.ReleaseObject();
                    _currentBlock = null;
                    _isGrabbed = false;
                }
            }
        }
    }

    public void Restart()
    {
        _currentBlock?.ReleaseObject();
        _isGrabbed = false;
        _currentBlock = null;
    }

    //Do it later, visual for laser
    /*
    private IEnumerator ShootLaserCoroutine()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _lineRenderer.enabled = false;
    }
    */

}
