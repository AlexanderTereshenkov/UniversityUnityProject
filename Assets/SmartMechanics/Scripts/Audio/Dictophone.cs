using Reflex.Attributes;
using UnityEngine;

public class Dictophone : MonoBehaviour, IInteractible, IRestartable
{
    [SerializeField] private AudioClip audioClip;

    private AudioService _audioService;
    private RespawnManager _respawnManager;
    private PuzzleAction _action;
    private float _playingTime;
    private float _playingTimer;
    private bool _isPlaying;

    [Inject]
    private void Construct(AudioService audioService, RespawnManager respawnManager)
    {
        _audioService = audioService;
        _respawnManager = respawnManager;
    }

    private void Start()
    {
        _action = GetComponent<PuzzleAction>();
        _respawnManager.Register(this);
        _playingTime = audioClip.length;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;
        _playingTimer += Time.deltaTime;
        Debug.Log("Timer: " + _playingTimer);
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

    public void Restart()
    {
        _audioService.PauseGlobalAudio();
        _isPlaying = false;
        _playingTimer = 0;
        _audioService.GlobalPlayedTime = 0;
    }
}
