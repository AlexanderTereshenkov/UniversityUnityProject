using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Player player;

    public Player GetPlayer() => player;
}
