using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{

    public string GetStringDescription()
    {
        return "Нажмите E, чтобы пополнить фильтры";
    }

    public void Interact(Inventory inventory)
    {
        inventory.Filters += 1;
        Destroy(gameObject);
    }

}
