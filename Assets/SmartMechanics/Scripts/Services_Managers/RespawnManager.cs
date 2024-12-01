using UnityEngine;
using System.Collections.Generic;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Player player;

    private List<Restartable> restartables = new();

    public void Register(Restartable restartable)
    {
        restartables.Add(restartable);
    }

    public void RestartObjects(bool resetLevel)
    {
        foreach (Restartable restartable in restartables)
        {
            restartable.Restart();
        }
        if (resetLevel)
        {
            player.GetPlayerMovement().TeleportPlayer(spawnPoint);
        }
    }
}
