using Reflex.Attributes;
using System;
using UnityEngine;


public class NegativeEffect : Restartable
{
    private float _radiationValue;
    private NegativeEffectUIProvider _uiProvider;
    private ViewUIManager _viewUIManager;
    private GameplayHandler _gameplayHandler;
    private RespawnManager _respawnManager;

    public Action<float> OnValueChanged;

    [Inject]
    private void Construct(ViewUIManager viewUIManager, GameplayHandler gameplayHandler, RespawnManager respawnManager)
    {
        _viewUIManager = viewUIManager;
        _gameplayHandler = gameplayHandler;
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        _uiProvider = new NegativeEffectUIProvider(this, _viewUIManager.GetView<DamageBarUI>());
        OnValueChanged?.Invoke(0);
        _respawnManager.Register(this);
    }

    public void ChangeValue(float value)
    {
        _radiationValue += value;
        _radiationValue = Mathf.Clamp(_radiationValue, 0, 100);
        OnValueChanged?.Invoke(_radiationValue);
        if(_radiationValue >= 100)
        {
            _gameplayHandler.LoseGame();
        }
    }

    public override void Restart(Player player)
    {
        throw new NotImplementedException();
    }

    public override void Restart()
    {
        _radiationValue = 0;
        OnValueChanged?.Invoke(_radiationValue);
    }
}
