using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gasmask : MonoBehaviour
{

    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private float workingTime;
    [SerializeField] private float signalTime;

    private InputAction _putAction;
    private InputAction _changeFilterAction;

    private float _workingTimeCounter;
    private bool _isSignalPlayed = false;
    private AudioService _audioService;

    public bool IsMaskOn { get; private set; }
    public int Filters { get; set; }

    [Inject]
    private void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Start()
    {
        IsMaskOn = false;
        _workingTimeCounter = workingTime;

        _putAction = inputActionAsset.FindAction("PutGasmask");

        _putAction.performed += PutGasmask;
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
                _audioService.PlayOneShotSound(AudioType.TimerEnd);
            }
        }
    }

    private void PutGasmask(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMaskOn = !IsMaskOn;
            if (IsMaskOn)
            {
                
            }
            else
            {

            }
        }
    }

}
