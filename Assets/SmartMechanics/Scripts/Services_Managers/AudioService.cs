using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioContainer[] audioContainer;
    [SerializeField] private AudioSource globalAudioSource;

    private Dictionary<AudioType, AudioClip> _clips = new Dictionary<AudioType, AudioClip>();
    private AudioSource _audioSource;
    
    public float GlobalPlayedTime
    {
        get; set;
    }


    private void Awake()
    {
        foreach (var clip in audioContainer)
        {
            _clips.Add(clip.containerType, clip.audioClip);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShotSound(AudioType audioType, float volume = 1f)
    {
        _audioSource.PlayOneShot(_clips[audioType], volume);
    }

    public void PlayGlobalAudio(AudioClip audioClip, float volume = 1f)
    {
        if (globalAudioSource.isPlaying)
        {
            return;
        }
        globalAudioSource.clip = audioClip;
        globalAudioSource.Play();
    }

    public void PauseGlobalAudio()
    {

        GlobalPlayedTime = globalAudioSource.time;
        globalAudioSource.Pause();
    }

    public void ContinueGlobalAudio()
    {
        globalAudioSource.time = GlobalPlayedTime;
        globalAudioSource.Play();
    }

}

[System.Serializable]
public struct AudioContainer
{
    public AudioType containerType;
    public AudioClip audioClip;
}

public enum AudioType
{
    GeigerCounter,
    TimerEnd,
    Gasmask,
    Step
}
