using Reflex.Attributes;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private Transform handPosition;

    private IPickable _pickableObject;
    private int _filters;
    private InventoryUIProvider _inventoryUIProvider;
    private ViewUIManager _viewUIManager;

    public event Action<IPickable> OnObjectChanged;
    public event Action<int> OnFilterChanged;

    [Inject]
    private void Construct(ViewUIManager viewUIManager)
    {
        _viewUIManager = viewUIManager;
    }

    private void Start()
    {
        _inventoryUIProvider = new InventoryUIProvider(_viewUIManager.GetView<MapInventoryPage>(), this);
        OnObjectChanged?.Invoke(null);
        OnFilterChanged?.Invoke(0);
    }

    public IPickable PickableObject
    {
        set
        {
            if(_pickableObject == null)
            {
                _pickableObject = value;
                OnObjectChanged?.Invoke(_pickableObject);
            }
        }
        get
        {
            return _pickableObject;
        }
    }

    public int Filters
    {
        get
        {
            return _filters;
        }
        set
        {
            _filters = value;
            OnFilterChanged?.Invoke(_filters);
        }
    }

    public Transform HandPosition
    {
        get
        {
            return handPosition;
        }
    }

    public void DeleteFromInventory()
    {
        if (_pickableObject == null)
            return;
        _pickableObject.Drop(this);
        _pickableObject = null;
        OnObjectChanged?.Invoke(_pickableObject);
    }


}
