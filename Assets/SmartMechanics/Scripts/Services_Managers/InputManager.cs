using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;

    public InputActionAsset ActionAsset { get { return _actionAsset; } }

    private void Start()
    {
        _actionAsset.Enable();
    }

    public void DisableActionMap(string map)
    {
        _actionAsset.FindActionMap(map).Disable();
    }

    public void EnableActionMap(string map)
    {
        _actionAsset.FindActionMap(map).Enable();
    }

}
