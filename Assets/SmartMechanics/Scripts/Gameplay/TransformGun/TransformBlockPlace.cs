using UnityEngine;

public class TransformBlockPlace : MonoBehaviour
{
    [SerializeField] private Transform blockPlace;
    public bool IsPlaced { get; private set; }

    public void SetBlock(TransformBlock block)
    {
        block.transform.parent = blockPlace;
        block.transform.localPosition = Vector3.zero;
        block.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
    }
}
