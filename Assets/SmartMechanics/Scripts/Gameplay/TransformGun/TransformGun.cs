using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformGun : MonoBehaviour
{

    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float gunDistance;
    [SerializeField] private LayerMask gunLayer;

    private InputActionAsset _inputActionAsset;
    private InputAction _shootAction;

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
            
        }
    }

    private bool CheckObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, transform.forward, out hit, gunDistance, gunLayer))
        {
            if(hit.collider.TryGetComponent(out TransformBlock transformBlock))
            {
                return true;
            }
        }
        return false;
    }
}
