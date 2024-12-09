using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSway : MonoBehaviour
{
    [SerializeField] private float swayMiltiplier;
    [SerializeField] private float swaySmooth;

    private InputAction _mouseAction;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _mouseAction = inputManager.ActionAsset.FindAction("Look");
    }

    private void Update()
    {
        float mouseX = Mathf.Clamp01(_mouseAction.ReadValue<Vector2>().x) * swayMiltiplier;
        float mouseY = Mathf.Clamp01(_mouseAction.ReadValue<Vector2>().y) * swayMiltiplier;

        var rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        var rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        var rotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, swaySmooth * Time.deltaTime);

    }
}
