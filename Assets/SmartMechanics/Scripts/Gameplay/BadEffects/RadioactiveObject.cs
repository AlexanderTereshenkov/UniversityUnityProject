using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RadioactiveObject : RadiationZone
{
    [SerializeField] private float minLivingTime;
    [SerializeField] private float maxLivingTime;

    private Rigidbody _rigidBody;
    private float _startTime;
    private float _livingTime;

    public override void Awake()
    {
        base.Awake();
        _rigidBody = GetComponent<Rigidbody>();
        _livingTime = Random.Range(minLivingTime, maxLivingTime);
        _startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (Time.time - _startTime > _livingTime)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyForce(Vector3 direction)
    {
        _rigidBody.AddForce(direction);
    }
}
