using UnityEngine;

public class OrderObject : PickableObject, IRestartable
{
    [SerializeField] private int posIndex;

    public override void Start()
    {
        base.Start();
        _respawnManager.Register(this);
    }

    public int GetPosIndex() => posIndex;

    public void Restart()
    {
        _player.GetInventory().DeleteFromInventory();
        transform.parent = null;
        _rigidbody.isKinematic = false;
        transform.position = _startPosition;
    }
}
