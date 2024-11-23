using Reflex.Attributes;
using UnityEngine;

public class Dictophone : MonoBehaviour, IInteractible
{
    [SerializeField] private AudioClip audioClip;
    private AudioService _audioService;

    [Inject]
    private void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    public string GetStringDescription()
    {
        return StringConstants.DefaultInteractibleDesc;
    }

    public void Interact(Inventory inventory)
    {
        _audioService.PlayAudio(audioClip);
    }

}
