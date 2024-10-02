using UnityEngine;
using Reflex.Attributes;

public class GeigerCounterAudio : MonoBehaviour
{

    public bool IsPlaying { get; set; }
    public float CoolDownTime { get; set; }

    private AudioService _audioService;
    private float _timer;

    [Inject]
    private void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Update()
    {
        if (!IsPlaying || CoolDownTime <= 0)
            return;

        _timer += Time.deltaTime;
        if (_timer >= CoolDownTime)
        {
            _audioService.PlayOneShotSound(AudioType.GeigerCounter, 0.3f);
            _timer = 0;
        }
    }
}
