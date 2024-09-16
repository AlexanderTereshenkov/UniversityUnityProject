using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject torch;

    [Header("Player and camera settings")]
    [SerializeField] private float speed;
    [SerializeField] private float mouseSens;
    [SerializeField] private float cameraAcceleration;
    [SerializeField] private float handAcceleration;
    [SerializeField] private float lookAngle;
    [Range(0, 10)]
    [SerializeField] private float runningScale;
    [Header("Camera bobbing")]
    [SerializeField] private float frequency;
    [SerializeField] private float frequencyScale;
    [SerializeField] private float amplitude;
    [SerializeField] private float amplitudeScale;
    [SerializeField] private float cameraReturnTime;
    

    private CharacterController _characterController;

    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _lookAction;
    private InputAction _toggleTorchAction;

    private float _verticalCameraRotation;
    private float _horizontalCameraRotation;
    private Vector3 _cameraStartLocalPosition;
    private float _headBobbingTimer;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        inputActionAsset.Enable();

        _moveAction = inputActionAsset.FindAction("Movement");
        _runAction = inputActionAsset.FindAction("Run");
        _lookAction = inputActionAsset.FindAction("Look");
        _toggleTorchAction = inputActionAsset.FindAction("ToggleTorch");

        _toggleTorchAction.performed += ToggleTorch;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cameraStartLocalPosition = playerCamera.transform.localPosition;
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>().normalized;

        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement = transform.TransformDirection(movement);
        movement *= speed * Time.deltaTime * (_runAction.IsPressed() ? runningScale : 1);

        if(movement.magnitude > 0)
        {
            _headBobbingTimer += Time.deltaTime;
            var tempFreq = frequency * (_runAction.IsPressed() ? frequencyScale : 1);
            var tempAmplitude = amplitude * (_runAction.IsPressed() ? amplitudeScale : 1);
            playerCamera.transform.localPosition = new Vector3
                (0,
                _cameraStartLocalPosition.y + (float)Math.Sin(_headBobbingTimer * tempFreq) * tempAmplitude,
                0);
        }
        else
        {
            _headBobbingTimer = 0;
            ReturnCameraPosition();
        }

        _characterController.Move(movement);

    }

    private void ReturnCameraPosition()
    {
        playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition,
            _cameraStartLocalPosition, Time.deltaTime * cameraReturnTime);
    }

    private void Look()
    {
        Vector2 mouseDelta = _lookAction.ReadValue<Vector2>();

        _verticalCameraRotation -= mouseDelta.y * Time.deltaTime * mouseSens;
        _verticalCameraRotation = Mathf.Clamp(_verticalCameraRotation, -lookAngle, lookAngle);
        _horizontalCameraRotation += mouseDelta.x * Time.deltaTime * mouseSens;

        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(0, _horizontalCameraRotation, 0), Time.deltaTime * cameraAcceleration);

        playerCamera.transform.localRotation = Quaternion.Euler(_verticalCameraRotation, 0, 0);

        hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation,
            Quaternion.Euler(_verticalCameraRotation, _horizontalCameraRotation, 0),
            Time.deltaTime * handAcceleration);
    }

    private void ToggleTorch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            torch.SetActive(!torch.activeInHierarchy);
        }
    }


}
