using Reflex.Core;
using UnityEngine;

public class GameplaySceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private AudioService audioService;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(audioService);
    }

}
