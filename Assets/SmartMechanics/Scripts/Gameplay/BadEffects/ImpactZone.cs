using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class ImpactZone : MonoBehaviour
{

    [SerializeField] protected float effectCoolDown;
    [SerializeField] protected float effectCoef;
    [SerializeField, Range(0, 100)] protected float minImpactValue;
    [SerializeField, Range(0, 100)] protected float maxImpactValue;

    protected bool _isPlayerInside;
    protected float _timer;
    protected SphereCollider _sphereCollider;
    protected float _radius;

    protected Player _currentplayer;

    public virtual void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _radius = _sphereCollider.radius;
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isPlayerInside = true;
            _currentplayer = player;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isPlayerInside = false;
            _currentplayer = null;
        }
    }
}
