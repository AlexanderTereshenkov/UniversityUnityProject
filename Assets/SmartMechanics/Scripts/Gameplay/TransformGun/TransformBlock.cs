using Reflex.Attributes;
using System;
using UnityEngine;


public class TransformBlock : MonoBehaviour, IRestartable
{
    [SerializeField] private TransformBlockManager manager;
    [SerializeField] private BlockType blockType;
    private bool _isGrabbed;
    private Transform _followTransform;
    private Vector3 _startPosition;
    private RespawnManager _respawnManager;

    public BlockType GetBlockType
    {
        get
        {
            return blockType;
        }
    }

    [Inject]
    private void Construct(RespawnManager respawnManager)
    {
        _respawnManager = respawnManager;
    }

    public event Action OnBlockGrabbed;

    private void Start()
    {
        _startPosition = transform.position;
        _respawnManager.Register(this);
    }

    private void Update()
    {
        if (!_isGrabbed)
            return;

        transform.position = Vector3.Slerp(transform.position, _followTransform.position, Time.deltaTime * 15);
        
    }

    public void GrabObject(Transform followTransform)
    {
        _isGrabbed = true;
        _followTransform = followTransform;
        gameObject.layer = 2;
        OnBlockGrabbed?.Invoke();
        manager.SetVisiblePlaces(true);
    }

    public void ReleaseObject()
    {
        _isGrabbed = false;
        _followTransform = null;
        gameObject.layer = 6;
        manager.SetVisiblePlaces(false);
    }

    public void Restart()
    {
        _isGrabbed = false;
        _followTransform = null;
        gameObject.layer = 6;
        manager.SetVisiblePlaces(false);
        transform.position = _startPosition;
    }
}
