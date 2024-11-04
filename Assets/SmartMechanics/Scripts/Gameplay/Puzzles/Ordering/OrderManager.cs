using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private int[] rightOrderIndex;
    [SerializeField] private ObjectPlace[] places;

    private int[] _currentOrder;
    private int _objectCount;
    private PuzzleAction _openDoorAction;

    private void Start()
    {
        _currentOrder = new int[rightOrderIndex.Length];
        for(int i = 0; i < rightOrderIndex.Length; i++)
        {
            places[i].PlaceIndex = i;
            places[i].OnObjectPlaced += AddObject;
            places[i].OnObjectRemoved += RemoveObject;
            _currentOrder[i] = -1;
        }
        _openDoorAction = GetComponent<PuzzleAction>();
    }

    private void AddObject(int index, OrderObject orderObject)
    {
        _currentOrder[index] = orderObject.GetPosIndex();
        _objectCount++;
        if(_objectCount >= rightOrderIndex.Length)
        {
            CheckOrder();
        }
    }

    private void RemoveObject(int index)
    {
        if(_objectCount >= rightOrderIndex.Length)
        {
            _openDoorAction.CancleAction();
        }
        _currentOrder[index] = -1;
        _objectCount--;
    }

    private void CheckOrder()
    {
        for(int i = 0; i < rightOrderIndex.Length; i++)
        {
            if (rightOrderIndex[i] != _currentOrder[i])
            {
                return;
            }
        }
        _openDoorAction.PerformAction();
    }

}
