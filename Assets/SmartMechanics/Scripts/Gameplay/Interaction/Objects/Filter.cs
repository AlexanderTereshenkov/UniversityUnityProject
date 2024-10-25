using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{

    [SerializeField] private string rayDescription;

    public string GetStringDescription()
    {
        return rayDescription;
    }

    public void Interact(Inventory inventory)
    {
        inventory.Filters += 1;
        Destroy(gameObject);
    }

}
