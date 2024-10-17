using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{

    public string GetStringDescription()
    {
        return "������� E, ����� ��������� �������";
    }

    public void Interact(Inventory inventory)
    {
        inventory.Filters += 1;
        Destroy(gameObject);
    }

}
