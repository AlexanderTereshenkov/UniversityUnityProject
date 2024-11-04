using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private int[] rightOrderIndex;
    [SerializeField] private ObjectPlace[] places;

    private int[] currentOrder;
    private int objectCount;

    private void Start()
    {
        currentOrder = new int[rightOrderIndex.Length];
        for(int i = 0; i < rightOrderIndex.Length; i++)
        {
            places[i].PlaceIndex = i;
            places[i].OnObjectPlaced += AddObject;
            places[i].OnObjectRemoved += RemoveObject;
            currentOrder[i] = -1;
        }
    }

    private void AddObject(int index, OrderObject orderObject)
    {
        currentOrder[index] = orderObject.GetPosIndex();
        objectCount++;
        if(objectCount >= rightOrderIndex.Length)
        {
            Debug.Log("CHECK NOOOOOW");
            CheckOrder();
        }
    }

    private void RemoveObject(int index)
    {
        currentOrder[index] = -1;
        objectCount--;
    }

    private void CheckOrder()
    {
        for(int i = 0; i < rightOrderIndex.Length; i++)
        {
            if (rightOrderIndex[i] != currentOrder[i])
            {
                Debug.Log("WRONG ORDER");
                break;
            }
        }
    }

}
