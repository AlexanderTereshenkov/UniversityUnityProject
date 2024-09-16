using UnityEngine;

public class RadioactiveZone : MonoBehaviour
{
    [SerializeField] private Transform epicenter;

    private bool _isPlayerInside;

    private void Update()
    {
        if (!_isPlayerInside)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RadiationEffect radiationEffect))
        {
            _isPlayerInside = true;
        }
    }
}
