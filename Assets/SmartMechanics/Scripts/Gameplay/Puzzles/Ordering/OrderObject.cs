using UnityEngine;

public class OrderObject : PickableObject
{
    [SerializeField] private int posIndex;

    public int GetPosIndex() => posIndex;
}
