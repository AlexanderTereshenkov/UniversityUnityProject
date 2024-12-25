using Reflex.Attributes;
using UnityEngine;

public class TransformBlockPlace : MonoBehaviour, IRestartable
{
    [SerializeField] private Transform blockPlace;
    [SerializeField] private Light placeLightMarker;
    [SerializeField] private BlockType blockType;
    private TransformBlock _block;
    private PuzzleAction _action;
    private RespawnManager _respawnManager;

    [Inject]
    private void Construct(RespawnManager respawnManager)
    {
        _respawnManager = respawnManager;
    }
    public bool IsPlaced { get; private set; }

    public BlockType GetBlockType
    {
        get
        {
            return blockType;
        }
    }

    private void Start()
    {
        placeLightMarker.enabled = false;
        _action = GetComponent<PuzzleAction>();
        _respawnManager.Register(this);
    }

    public void SetBlock(TransformBlock block)
    {
        if (IsPlaced || block.GetBlockType != blockType)
            return;
        _block = block;
        _block.transform.parent = blockPlace;
        _block.transform.localPosition = Vector3.zero;
        _block.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _block.OnBlockGrabbed += OnGrabObject;
        ShowHidePlace(false);
        IsPlaced = true;
        if(_action != null)
            _action.PerformAction();
    }

    public void ShowHidePlace(bool isVisible)
    {
        placeLightMarker.enabled = isVisible;
    }

    private void OnGrabObject()
    {
        IsPlaced = false;
        if(_block != null)
        {
            _block.OnBlockGrabbed -= OnGrabObject;
            _block = null;
        }
        if(_action != null)
            _action.CancleAction();
    }

    public void Restart()
    {
        OnGrabObject();
    }
}

public enum BlockType
{
    Red,
    Yellow
}
