using Reflex.Attributes;
using UnityEngine;

public class Dictophone : MonoBehaviour, IInteractible
{
    [SerializeField] private AudioClip audioClip;

    private AudioService _audioService;
    private PuzzleAction _action;
    private float _playingTime;
    private float _playingTimer;
    private bool _isPlaying;

    [Inject]
    private void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Start()
    {
        _action = GetComponent<PuzzleAction>();
        _playingTime = audioClip.length;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;
        _playingTimer += Time.deltaTime;
        if(_playingTimer >= _playingTime)
        {
            if(_action != null)
            {
                _action.PerformAction();
            }
            _isPlaying = false;
            _playingTimer = 0;
        }
    }
    public string GetStringDescription()
    {
        return StringConstants.DefaultInteractibleDesc;
    }

    public void Interact(Inventory inventory)
    {
        PlayAudio();
    }

    private void PlayAudio()
    {
        _audioService.PlayGlobalAudio(audioClip);
        _isPlaying = true;
    }

}
