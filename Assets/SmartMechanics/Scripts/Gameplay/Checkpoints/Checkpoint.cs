using Reflex.Attributes;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;

    private Player _currentPlayer;
    private CheckpointManager _checkpointManager;
    private RespawnManager _respawnManager;

    [Inject]
    private void Construct(CheckpointManager checkpointManager, RespawnManager respawnManager)
    {
        _checkpointManager = checkpointManager;
        _respawnManager = respawnManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentPlayer != null)
            return;
        if(other.gameObject.TryGetComponent(out Player player))
        {
            _currentPlayer = player;
            _checkpointManager.CurrentCheckpoint = this;
        }
    }

    public void Restart()
    {
        _respawnManager.RestartObjects(false);
        _currentPlayer.GetPlayerMovement().TeleportPlayer(spawnPoint);
    }
}
