using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioContainer[] audioContainer;

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
    TimerEnd
}
