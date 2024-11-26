using Cinemachine;
using Reflex.Attributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject torch;
    [Header("Ground checking")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private float sphereRadius;

    [Header("Player and camera settings")]
    [SerializeField] private float speed;
    [SerializeField] private float mouseSens;
    [SerializeField] private float gravityScale;
    [SerializeField] private float cameraAcceleration;
    [SerializeField] private float handAcceleration;
    [SerializeField] private float lookAngle;
    [Range(0, 10)]
    [SerializeField] private float runningScale;
    [SerializeField] private float jumpHeight;
    [Header("Camera bobbing")]
    [SerializeField] private float frequency;
    [SerializeField] private float frequencyScale;
    [SerializeField] private float amplitude;
    [SerializeField] private float amplitudeScale;
    [SerializeField] private float cameraReturnTime;
    

    private CharacterController _characterController;

    private InputActionAsset _inputActionAsset;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _lookAction;
    private InputAction _toggleTorchAction;
    private InputAction _jumpAction;

    private float _verticalCameraRotation;
    private float _horizontalCameraRotation;
    private Vector3 _cameraStartLocalPosition;
    private float _headBobbingTimer;
    private bool _isGrounded;
    private AudioService _audioService;
    private Animator _animator;

    private Vector3 _velocity;

    [Inject]
    private void Construct(AudioService audioService, InputManager inputManager)
    {
        _audioService = audioService;
        _inputActionAsset = inputManager.ActionAsset;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _moveAction = _inputActionAsset.FindAction("Movement");
        _runAction = _inputActionAsset.FindAction("Run");
        _lookAction = _inputActionAsset.FindAction("Look");
        _toggleTorchAction = _inputActionAsset.FindAction("ToggleTorch");
        _jumpAction = _inputActionAsset.FindAction("Jump");

        _toggleTorchAction.performed += ToggleTorch;
        //_jumpAction.performed += JumpAction;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cameraStartLocalPosition = playerCamera.transform.localPosition;
        _animator = GetComponent<Animator>();
        ignoreLayer = ~ignoreLayer;
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {

        _isGrounded = CheckGround();

        //movement
        Vector2 input = _moveAction.ReadValue<Vector2>().normalized;

        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement = transform.TransformDirection(movement);
        movement *= speed * Time.deltaTime * (_runAction.IsPressed() ? runningScale : 1);

        _animator.SetFloat("Speed", movement.magnitude);

        if (!_isGrounded)
        {
            _animator.SetFloat("Speed", 0);
        }

        _animator.SetBool("Run", _isGrounded & _runAction.IsPressed());

        if (movement.magnitude > 0)
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

        //jumping and gravity
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }

        if(_isGrounded && _jumpAction.WasPerformedThisFrame())
        {
            _velocity.y = (float)Math.Sqrt(-2f * jumpHeight * gravityScale);
        }

        _velocity.y += gravityScale * Time.deltaTime;

        _characterController.Move(Time.deltaTime * _velocity);

        Debug.DrawRay(groundCheckTransform.position, Vector3.down * sphereRadius);

    }

    public void PlayStepSound()
    {
        _audioService.PlayOneShotSound(AudioType.Step);
    }

    private void ReturnCameraPosition()
    {
        playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition,
            _cameraStartLocalPosition, Time.deltaTime * cameraReturnTime);
    }

    private bool CheckGround()
    {
        return Physics.Raycast(groundCheckTransform.position, Vector3.down, sphereRadius, ignoreLayer);
    }

    private void Look()
    {
        Vector2 mouseDelta = _lookAction.ReadValue<Vector2>();

        _verticalCameraRotation -= mouseDelta.y * Time.deltaTime * mouseSens;
        _verticalCameraRotation = Mathf.Clamp(_verticalCameraRotation, -lookAngle, lookAngle);
        _horizontalCameraRotation += mouseDelta.x * Time.deltaTime * mouseSens;

        transform.rotation = Quaternion.Euler(0, _horizontalCameraRotation, 0);

        //transform.rotation = Quaternion.Lerp(transform.rotation,
        //    Quaternion.Euler(0, _horizontalCameraRotation, 0), Time.deltaTime * cameraAcceleration);

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
