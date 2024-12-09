using UnityEngine;
using System.Collections.Generic;
using Reflex.Attributes;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private Player _player;
    private List<IRestartable> restartables = new();

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    public void Register(IRestartable restartable)
    {
        restartables.Add(restartable);
    }

    public void RestartObjects(bool resetLevel)
    {
        foreach (IRestartable restartable in restartables)
        {
            restartable.Restart();
        }
        if (resetLevel)
        {
            _player.GetPlayerMovement().TeleportPlayer(spawnPoint);
        }
        _player.GetGeigerCounter().CoolDownTime = 0;
        _player.GetGeigerCounter().IsPlaying = false;
    }
}
