using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private NegativeEffect negativeEffect;
    [SerializeField] private GeigerCounterAudio geigerCounter;
    [SerializeField] private Gasmask playerGasmask;
    [SerializeField] private Inventory inventory;

    public NegativeEffect GetNegativeEffect() => negativeEffect;
    public GeigerCounterAudio GetGeigerCounter() => geigerCounter;
    public Gasmask GetPlayerGasmask() => playerGasmask;
    public Inventory GetInventory() => inventory;
}
