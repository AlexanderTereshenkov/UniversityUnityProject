using Reflex.Attributes;
using UnityEngine;

public class Pills : MonoBehaviour, IInteractible
{
    [SerializeField] private float healValue;

    private NegativeEffect _negativeEffect;

    [Inject]
    private void Construct(Player player)
    {
        _negativeEffect = player.GetNegativeEffect();
    }

    public string GetStringDescription()
    {
        return StringConstants.PillsInteractibleDesc;
    }

    public void Interact(Inventory inventory)
    {
        _negativeEffect.ChangeValue(-healValue);
        Destroy(gameObject);
    }

}
