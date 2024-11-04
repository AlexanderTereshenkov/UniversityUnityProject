using Reflex.Attributes;
using UnityEngine;

public class Meds : MonoBehaviour, IInteractible
{
    [SerializeField] private float healValue;
    [SerializeField] private string description; 

    private NegativeEffect _negativeEffect;

    [Inject]
    private void Construct(Player player)
    {
        _negativeEffect = player.GetNegativeEffect();
    }

    public string GetStringDescription()
    {
        return description;
    }

    public void Interact(Inventory inventory)
    {
        _negativeEffect.ChangeValue(-healValue);
        Destroy(gameObject);
    }

}
