
public class NegativeEffectUIProvider
{
    private NegativeEffect _negativeEffect;
    private DamageBarUI _damageBarUI;

    public NegativeEffectUIProvider(NegativeEffect negativeEffect, DamageBarUI damageBarUI)
    {
        _negativeEffect = negativeEffect;
        _damageBarUI = damageBarUI;
        _negativeEffect.OnValueChanged += SetValue;
    }

    private void SetValue(float value)
    {
        _damageBarUI.FillBar(100, value);
    }

    ~NegativeEffectUIProvider()
    {
        _negativeEffect.OnValueChanged -= SetValue;
    }

}
