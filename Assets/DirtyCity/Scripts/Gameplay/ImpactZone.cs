using UnityEngine;

public class ImpactZone : MonoBehaviour
{

    [SerializeField] private float effectCoolDown;
    [SerializeField] private float effectCoef;
    [SerializeField, Range(0, 100)] private float minImpactValue;
    [SerializeField, Range(0, 100)] private float maxImpactValue;

    private bool _isPlayerInside;
    private float _timer;
    private NegativeEffect _currentRadiationEffect;
    private SphereCollider _sphereCollider;
    private float _radius;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _radius = _sphereCollider.radius;
        Debug.Log(_radius);
    }

    private void Update()
    {
        if (!_isPlayerInside)
        {
            return;
        }
        _timer += Time.deltaTime;
        if (_currentRadiationEffect != null && _timer >= effectCoolDown)
        {
            float distance = Vector3.Distance(transform.position, _currentRadiationEffect.transform.position);
            float value = effectCoef - (distance / _radius);
            _currentRadiationEffect.ChangeValue(Random.Range(minImpactValue, maxImpactValue) * value);
            _timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out NegativeEffect radiationEffect))
        {
            _isPlayerInside = true;
            _currentRadiationEffect = radiationEffect;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NegativeEffect radiationEffect))
        {
            _isPlayerInside = false;
            _currentRadiationEffect = null;
        }
    }
}
