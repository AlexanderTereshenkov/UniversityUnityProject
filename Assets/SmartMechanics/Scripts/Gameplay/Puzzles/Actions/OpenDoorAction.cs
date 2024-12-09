using Reflex.Attributes;
using UnityEngine;

public class OpenDoorAction : PuzzleAction, IRestartable
{

    [SerializeField] private GameObject door;

    private Animator _animator;
    private bool _isOpened;
    private RespawnManager _respawnManager;

    [Inject]
    private void Construct(RespawnManager respawnManager)
    {
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        if (door.TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
        _respawnManager.Register(this);
    }

    public override void CancleAction()
    {
        _isOpened = false;
        _animator.SetBool("isOpen", false);
    }

    public override void PerformAction()
    {
        _isOpened = true;
        _animator.SetBool("isOpen", true);
    }

    public void Restart()
    {
        if (_isOpened)
        {
            CancleAction();
        }
    }

}
