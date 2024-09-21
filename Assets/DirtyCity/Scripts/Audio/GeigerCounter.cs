using UnityEngine;


public class GeigerCounter : MonoBehaviour
{

    public bool IsPlaying { get; set; }
    public float CoolDownTime { get; set; }

    private AudioManager _audioManager;
    private float _timer;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (!IsPlaying || CoolDownTime <= 0)
            return;

        _timer += Time.deltaTime;
        if (_timer >= CoolDownTime)
        {
            _audioManager.PlayOneShotSound(AudioType.GeigerCounter, 0.3f);
            _timer = 0;
        }
    }
}
