using UnityEngine;

public class ObjectPlacer : MonoBehaviour, IInteractible
{
    [SerializeField] private string key;
    [SerializeField] private Transform objectPlace;

    private bool _isFull;

    public string GetStringDescription()
    {
        return StringConstants.DefaultInteractibleDesc;
    }

    public void Interact(Inventory inventory)
    {
        if(inventory.PickableObject == null || _isFull) 
            return;

        if(inventory.PickableObject.GetKey() == key)
        {
            var currentGameObject = inventory.PickableObject.GetGameObject();
            currentGameObject.GetComponent<PickableObject>().enabled = false;
            currentGameObject.transform.SetParent(objectPlace, false);
            currentGameObject.transform.position = objectPlace.position;
            currentGameObject.transform.rotation = objectPlace.rotation;
            inventory.DeleteFromInventory();
            _isFull = true;
        }
    }

}
