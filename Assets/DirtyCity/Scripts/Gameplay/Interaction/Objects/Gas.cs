using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Gas : MonoBehaviour, IInteractible, IPickable
{

    [SerializeField] private string objectName;
    [SerializeField] private Sprite objectSprite;

    private Rigidbody _rigidbody;

    private void Start()
    {

        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Drop(Inventory inventory)
    {
        gameObject.transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * 5);
    }

    public GameObject GetCurrentGameObject()
    {
        throw new System.NotImplementedException();
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
        return "Нажмите E, чтобы взять канистру";
    }

    public void Interact(Inventory inventory)
    {
        Pick(inventory);
    }

    public void Pick(Inventory inventory)
    {
        inventory.PickableObject = this;
        gameObject.transform.parent = inventory.HandPosition;
        gameObject.transform.position = inventory.HandPosition.position;
        _rigidbody.isKinematic = true;
    }
}
