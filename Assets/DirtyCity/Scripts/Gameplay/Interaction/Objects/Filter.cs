using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{
    public string GetStringDescription()
    {
        return "������� E";
    }

    public void Interact()
    {
        Debug.Log("Filter picked");
    }
}
