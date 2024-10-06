using UnityEngine;
using UnityEngine.Rendering;

public class WorldSettings : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    public Volume GetGlobalVolume() => globalVolume;
}
