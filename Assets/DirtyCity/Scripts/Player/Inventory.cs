using Reflex.Attributes;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private IInteractible _pickableObject;
    private int _filters;

    private InventoryUIProvider _inventoryUIProvider;
    private ViewUIManager _viewUIManager;

    public event Action<IInteractible> OnObjectPicked;
    public event Action<int> OnFilterChanged;

    [Inject]
    private void Construct(ViewUIManager viewUIManager)
    {
        _viewUIManager = viewUIManager;
    }

    private void Start()
    {
        _inventoryUIProvider = new InventoryUIProvider(_viewUIManager.GetView<MapInventoryPage>(), this);
        OnObjectPicked?.Invoke(null);
        OnFilterChanged?.Invoke(0);
    }

    public IInteractible PickableObject
    {
        set
        {
            if(_pickableObject == null)
            {
                _pickableObject = value;
                OnObjectPicked?.Invoke(_pickableObject);
            }
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

    public IInteractible GetPickableObject()
    {
        var returnObject = _pickableObject;
        _pickableObject = null;
        OnObjectPicked?.Invoke(_pickableObject);
        return returnObject;
    }

}
