using UnityEngine;

public class Filter : MonoBehaviour, IInteractible
{
    public string GetStringDescription()
    {
        return "ֽאזלטעו E";
    }

    public void Interact()
    {
        Debug.Log("Filter picked");
    }
}
