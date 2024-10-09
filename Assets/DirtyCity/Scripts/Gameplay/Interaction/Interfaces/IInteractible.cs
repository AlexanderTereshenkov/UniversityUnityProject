using UnityEngine;

public interface IInteractible
{
    public void Interact(Inventory inventory);
    public void DeleteFromInventory(Inventory inventory);
    public string GetStringDescription();
    public Sprite GetIcon();
    public string GetName();

}
