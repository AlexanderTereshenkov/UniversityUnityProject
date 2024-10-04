using UnityEngine;

public class RadioactiveWindZone : ImpactZone
{
    private NegativeEffect _negativeEffect;
    private Gasmask _playergasmask;

    private void Update()
    {
        if (!_isPlayerInside)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_currentplayer != null && _timer >= effectCoolDown)
        {
            float damage = Random.Range(minImpactValue, maxImpactValue);
            if (_playergasmask.IsMaskOn)
            {
                _negativeEffect.ChangeValue(damage * effectCoef);
            }
            else
            {
                _negativeEffect.ChangeValue(damage / 2f);
            }
            _timer = 0;
        }
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            base.OnTriggerEnter(other);

            _negativeEffect = _currentplayer.GetNegativeEffect();
            _playergasmask = _currentplayer.GetPlayerGasmask();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            base.OnTriggerExit(other);

            _negativeEffect = null;
            _playergasmask = null;
        }
    }

}
