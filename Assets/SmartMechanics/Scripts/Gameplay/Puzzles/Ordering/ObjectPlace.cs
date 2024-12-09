using Reflex.Attributes;
using System;
using UnityEngine;

public class ObjectPlace : MonoBehaviour, IInteractible, IRestartable
{
    [SerializeField] private string key;
    [SerializeField] private string description;
    [SerializeField] private Transform objectPlace;

    private bool _isFull;
    private PickableObject _currentObject;
    private RespawnManager _respawnManager;

    public int PlaceIndex { get; set; }

    public event Action<int, OrderObject> OnObjectPlaced;
    public event Action<int> OnObjectRemoved;

    [Inject]
    private void Construct(RespawnManager respawnManager)
    {
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        _respawnManager.Register(this);
    }

    public string GetStringDescription()
    {
        return description;
    }

    public void Interact(Inventory inventory)
    {
        if(inventory.PickableObject == null || _isFull)
        {
            return;
        }
        if (inventory.PickableObject.GetKey() == key)
        {
            var currentGameObject = inventory.PickableObject.GetGameObject();
            currentGameObject.transform.SetParent(objectPlace, false);
            currentGameObject.transform.position = objectPlace.position;
            currentGameObject.transform.rotation = objectPlace.rotation;
            inventory.DeleteFromInventory();
            if(currentGameObject.TryGetComponent(out OrderObject orderObject))
            {
                OnObjectPlaced?.Invoke(PlaceIndex, orderObject);
            }
            if(currentGameObject.TryGetComponent(out PickableObject pickableObject))
            {
                pickableObject.OnObjectPicked += ObjectPicked;
                _currentObject = pickableObject;
            }
            _isFull = true;
        }
    }

    public void Restart()
    {
        if(_currentObject != null)
        {
            _currentObject.OnObjectPicked -= ObjectPicked;
            OnObjectRemoved?.Invoke(PlaceIndex);
            _currentObject = null;
        }
        _isFull = false; 
    }

    private void ObjectPicked()
    {
        _currentObject.OnObjectPicked -= ObjectPicked;
        OnObjectRemoved?.Invoke(PlaceIndex);
        _isFull = false;
        _currentObject = null;
    }
}
