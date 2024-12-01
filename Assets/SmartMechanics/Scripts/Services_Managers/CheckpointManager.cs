using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint CurrentCheckpoint { get; set; }

    public void ReloadCurrentCheckpoint()
    {
        CurrentCheckpoint.Restart();
    }
}
