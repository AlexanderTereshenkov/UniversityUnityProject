using UnityEngine;

public class OpenDoorAction : PuzzleAction
{

    [SerializeField] private GameObject door;

    private Animator _animator;
    private bool _isOpened;

    private void Start()
    {
        if (door.TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
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

}
