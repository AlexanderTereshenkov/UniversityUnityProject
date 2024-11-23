using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractible
{

    private PuzzleAction _action;
    private bool _isOpen;

    private void Start()
    {
        _action = GetComponent<OpenDoorAction>();
    }

    public string GetStringDescription()
    {
        return StringConstants.DefaultInteractibleDesc;
    }

    public void Interact(Inventory inventory)
    {
        if (!_isOpen)
        {
            _action.PerformAction();
            _isOpen = true;
        }
        else
        {
            _action.CancleAction();
            _isOpen = false;
        }

    }
}
