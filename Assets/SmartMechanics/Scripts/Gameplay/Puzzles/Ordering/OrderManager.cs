using Reflex.Attributes;
using UnityEngine;

public class OrderManager : MonoBehaviour, IRestartable
{
    [SerializeField] private int[] rightOrderIndex;
    [SerializeField] private ObjectPlace[] places;

    private int[] _currentOrder;
    private int _objectCount;
    private PuzzleAction _puzzleAction;
    private RespawnManager _respawnManager;

    [Inject]
    private void Construct(RespawnManager respawnManager)
    {
        _respawnManager = respawnManager;
    }

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
        _puzzleAction = GetComponent<PuzzleAction>();
        _respawnManager.Register(this);
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
            _puzzleAction.CancleAction();
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
        _puzzleAction.PerformAction();
    }

    public void Restart()
    {
        for (int i = 0; i < rightOrderIndex.Length; i++)
        {
            places[i].PlaceIndex = i;
            places[i].OnObjectPlaced += AddObject;
            places[i].OnObjectRemoved += RemoveObject;
            _currentOrder[i] = -1;
        }
        _objectCount = 0;
    }
}
