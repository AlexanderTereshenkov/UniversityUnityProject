using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Gasmask : MonoBehaviour
{

    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private float workingTime;
    [SerializeField] private float signalTime;
    [Range(0, 1)]
    [SerializeField] private float minVignette;
    [Range(0, 1)]
    [SerializeField] private float maxVignette;

    private InputAction _putAction;
    private InputAction _changeFilterAction;

    private float _workingTimeCounter;
    private bool _isSignalPlayed = false;
    private AudioService _audioService;
    private WorldSettings _worldSettings;

    public bool IsMaskOn { get; private set; }
    public bool IsMaskWorking { get; private set; }
    public int Filters { get; set; }

    [Inject]
    private void Construct(AudioService audioService, WorldSettings worldSettings)
    {
        _audioService = audioService;
        _worldSettings = worldSettings;
    }

    private void Start()
    {
        IsMaskOn = false;
        IsMaskWorking = true;
        Filters = 3;
        _workingTimeCounter = workingTime;

        if(_worldSettings.GetGlobalVolume().profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(minVignette);
        }

        _putAction = inputActionAsset.FindAction("PutGasmask");
        _changeFilterAction = inputActionAsset.FindAction("ChangeFilter");

        _putAction.performed += PutGasmask;
        _changeFilterAction.performed += ChangeFilter;
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
            if(Filters - 1 >= 0)
            {
                IsMaskWorking = true;
                Filters--;
                _workingTimeCounter = workingTime;
                _isSignalPlayed = false;
            } 
        }
    }

}
