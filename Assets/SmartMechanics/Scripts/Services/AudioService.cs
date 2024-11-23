using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioContainer[] audioContainer;
    //Main Audio Source. Made for long audio
    [SerializeField] private AudioSource mainAudioSource;

    private Dictionary<AudioType, AudioClip> _clips = new Dictionary<AudioType, AudioClip>();
    private AudioSource _audioSource;

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

    public void PlayAudio(AudioClip audioClip, float volume = 1f)
    {
        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.clip = audioClip;
            mainAudioSource.volume = volume;
            mainAudioSource.Play();
        }
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
