using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{

    public string GetStringDescription()
    {
        return StringConstants.FilerInteractibleDesc;
    }

    public void Interact(Inventory inventory)
    {
        inventory.Filters += 1;
        Destroy(gameObject);
    }

}
