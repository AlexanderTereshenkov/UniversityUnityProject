using UnityEngine;

public class TransformBlockPlace : MonoBehaviour
{
    [SerializeField] private Transform blockPlace;
    [SerializeField] private Light placeLightMarker;
    private TransformBlock _block;
    private PuzzleAction _action;
    public bool IsPlaced { get; private set; }

    private void Start()
    {
        placeLightMarker.enabled = false;
        _action = GetComponent<PuzzleAction>();
    }

    public void SetBlock(TransformBlock block)
    {
        if (IsPlaced)
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
}
