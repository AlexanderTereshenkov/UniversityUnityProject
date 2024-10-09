using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{

    public Sprite GetIcon()
    {
        return null;
    }

    public string GetName()
    {
        return string.Empty;
    }

    public string GetStringDescription()
    {
        return "Нажмите E, чтобы пополнить фильтры";
    }

    public string GetDescription()
    {
        return string.Empty;
    }

    public void Interact(Inventory inventory)
    {
        inventory.Filters += 1;
        Destroy(gameObject);
    }

    public void DeleteFromInventory(Inventory inventory)
    {

    }
}
