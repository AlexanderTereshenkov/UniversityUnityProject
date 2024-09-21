using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private NegativeEffect negativeEffect;
    [SerializeField] private GeigerCounter geigerCounter;

    public NegativeEffect GetNegativeEffect() => negativeEffect;

    public GeigerCounter GetGeigerCounter() => geigerCounter;
}
