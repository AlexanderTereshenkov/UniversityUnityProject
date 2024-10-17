using UnityEngine;

public interface IInteractible
{
    public void Interact(Inventory inventory);
    public string GetStringDescription();

}
