using Reflex.Attributes;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PickableObject : MonoBehaviour, IInteractible, IPickable
{

    [SerializeField] private string objectName;
    [SerializeField] private Sprite objectSprite;
    [SerializeField] private float throwForce;
    [SerializeField] private string rayDesription;
    [Header("Key for object placing")]
    [SerializeField] private string objectKey;

    protected Player _player;
    protected RespawnManager _respawnManager;
    protected Vector3 _startPosition;
    protected Rigidbody _rigidbody;

    public bool IsPickable { get; set; }

    public event Action OnObjectPicked;

    [Inject]
    private void Construct(Player player, RespawnManager respawnManager)
    {
        _player = player;
        _respawnManager = respawnManager;
    }

    public virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        IsPickable = true;
        _startPosition = transform.position;
    }

    public void Drop()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * throwForce);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Sprite GetIcon()
    {
        return objectSprite;
    }

    public string GetName()
    {
        return objectName;
    }

    public string GetStringDescription()
    {
        return rayDesription;
    }

    public void Interact(Inventory inventory)
    {
        if (!IsPickable)
        {
            return;
        }
        Pick(inventory);
    }

    public void Pick(Inventory inventory)
    {
        if (inventory.PickableObject != null)
        {
            return;
        }
        inventory.PickableObject = this;
        gameObject.transform.parent = inventory.HandPosition;
        gameObject.transform.position = inventory.HandPosition.position;
        gameObject.transform.rotation = inventory.HandPosition.rotation;
        _rigidbody.isKinematic = true;
        OnObjectPicked?.Invoke();
    }

    public string GetKey()
    {
        return objectKey;
    }
}
