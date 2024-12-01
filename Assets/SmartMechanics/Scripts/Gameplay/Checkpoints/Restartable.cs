using UnityEngine;

public abstract class Restartable : MonoBehaviour
{
    public abstract void Restart(Player player);
    public abstract void Restart();
}
