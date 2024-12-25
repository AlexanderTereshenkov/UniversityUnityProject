using Reflex.Attributes;
using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractible, IRestartable
{
    private PuzzleAction _action;
    private bool _isOpen;
    private RespawnManager _respawnManager;

    [Inject]
    private void Construct(RespawnManager respawnManager)
    {
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        _action = GetComponent<OpenDoorAction>();
        _respawnManager.Register(this);
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

    public void Restart()
    {
        _isOpen = false;
        _action.CancleAction();
    }
}
