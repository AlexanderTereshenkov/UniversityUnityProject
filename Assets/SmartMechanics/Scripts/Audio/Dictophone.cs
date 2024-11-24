using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Dictophone : MonoBehaviour, IInteractible
{
    [SerializeField] private AudioClip audioClip;

    private AudioSource _audioSorce;

    private void Start()
    {
        _audioSorce = GetComponent<AudioSource>();
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
        if (!_audioSorce.isPlaying)
        {
            _audioSorce.clip = audioClip;
            _audioSorce.Play();
        }
    }

}
