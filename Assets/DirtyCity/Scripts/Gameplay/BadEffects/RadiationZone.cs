using UnityEngine;

public class RadiationZone : ImpactZone
{

    private NegativeEffect _negativeEffect;
    private GeigerCounter _geigerCounter;

    private void Update()
    {
        if (!_isPlayerInside)
        {
            return;
        }

        _timer += Time.deltaTime;

        float geigerDistance = Vector3.Distance(transform.position, _currentplayer.transform.position);
        _geigerCounter.CoolDownTime = geigerDistance / (_radius * 2f);

        if (_currentplayer != null && _timer >= effectCoolDown)
        {
            float distance = Vector3.Distance(transform.position, _currentplayer.transform.position);
            float value = effectCoef - (distance / _radius);

            _negativeEffect.ChangeValue(Random.Range(minImpactValue, maxImpactValue) * value);
            _timer = 0;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            base.OnTriggerEnter(other);

            _negativeEffect = _currentplayer.GetNegativeEffect();
            _geigerCounter = _currentplayer.GetGeigerCounter();

            _geigerCounter.IsPlaying = true;
            float distance = Vector3.Distance(transform.position, _currentplayer.transform.position);
            _geigerCounter.CoolDownTime = distance / _radius;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            base.OnTriggerExit(other);

            _geigerCounter.IsPlaying = false;
            _negativeEffect = null;
            _geigerCounter = null;
        }
    }
}
