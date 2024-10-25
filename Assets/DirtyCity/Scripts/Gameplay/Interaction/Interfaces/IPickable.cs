using UnityEngine;

public interface IPickable
{
    public void Pick(Inventory inventory);
    public void Drop();
    public string GetKey();
    public GameObject GetGameObject();
    public Sprite GetIcon();
    public string GetName();
}
