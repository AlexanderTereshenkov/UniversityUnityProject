using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Gasmask : Restartable
{

    [SerializeField] private float workingTime;
    [SerializeField] private float signalTime;
    [Range(0, 1)]
    [SerializeField] private float minVignette;
    [Range(0, 1)]
    [SerializeField] private float maxVignette;
    [SerializeField] private Inventory inventory;

    private InputActionAsset _inputActionAsset;
    private InputAction _putAction;
    private InputAction _changeFilterAction;

    private float _workingTimeCounter;
    private bool _isSignalPlayed = false;
    private AudioService _audioService;
    private WorldSettings _worldSettings;
    private RespawnManager _respawnManager;

    public bool IsMaskOn { get; private set; }
    public bool IsMaskWorking { get; private set; }


    [Inject]
    private void Construct(AudioService audioService, WorldSettings worldSettings, InputManager inputManager, 
        RespawnManager respawnManager)
    {
        _audioService = audioService;
        _worldSettings = worldSettings;
        _inputActionAsset = inputManager.ActionAsset;
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        IsMaskOn = false;
        IsMaskWorking = true;
        _workingTimeCounter = workingTime;

        if(_worldSettings.GetGlobalVolume().profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(minVignette);
        }

        _putAction = _inputActionAsset.FindAction("PutGasmask");
        _changeFilterAction = _inputActionAsset.FindAction("ChangeFilter");

        _putAction.performed += PutGasmask;
        _changeFilterAction.performed += ChangeFilter;
        _respawnManager.Register(this);
    }

    private void Update()
    {
        if (!IsMaskOn)
        {
            return;
        }
        if(_workingTimeCounter > 0)
        {
            _workingTimeCounter -= Time.deltaTime;
            if(!_isSignalPlayed && _workingTimeCounter <= signalTime)
            {
                _isSignalPlayed = true;
                IsMaskWorking = false;
                _audioService.PlayOneShotSound(AudioType.TimerEnd);
            }
        }
    }

    private void PutGasmask(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMaskOn = !IsMaskOn;

            if(_worldSettings.GetGlobalVolume().profile.TryGet(out Vignette vignette))
            {
                vignette.intensity.Override(IsMaskOn ? maxVignette : minVignette);
            }

            _audioService.PlayOneShotSound(AudioType.Gasmask);
        }
    }

    private void ChangeFilter(InputAction.CallbackContext context)
    {
        if (IsMaskOn && !IsMaskWorking)
        {
            if(inventory.Filters - 1 >= 0)
            {
                IsMaskWorking = true;
                inventory.Filters--;
                _workingTimeCounter = workingTime;
                _isSignalPlayed = false;
            } 
        }
    }

    public override void Restart(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Restart()
    {
        IsMaskOn = false;
        if (_worldSettings.GetGlobalVolume().profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(IsMaskOn ? maxVignette : minVignette);
        }
    }
}
