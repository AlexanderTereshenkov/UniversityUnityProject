using Reflex.Attributes;
using System;
using UnityEngine;


public class NegativeEffect : MonoBehaviour
{

    private float _radiationValue;
    private NegativeEffectUIProvider _uiProvider;
    private ViewUIManager _viewUIManager;

    public Action<float> OnValueChanged;

    [Inject]
    private void Construct(ViewUIManager viewUIManager)
    {
        _viewUIManager = viewUIManager;
    }

    private void Start()
    {
        _uiProvider = new NegativeEffectUIProvider(this, _viewUIManager.GetView<DamageBarUI>());
        OnValueChanged?.Invoke(0);
    }

    public void ChangeValue(float value)
    {
        _radiationValue += value;
        OnValueChanged?.Invoke(_radiationValue);
    }
}
