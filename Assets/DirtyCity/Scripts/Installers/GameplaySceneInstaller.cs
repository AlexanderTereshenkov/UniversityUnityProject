using Reflex.Core;
using UnityEngine;

public class GameplaySceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private AudioService audioService;
    [SerializeField] private ViewUIManager viewUiManager;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(audioService);
        containerBuilder.AddSingleton(viewUiManager);
    }

}
