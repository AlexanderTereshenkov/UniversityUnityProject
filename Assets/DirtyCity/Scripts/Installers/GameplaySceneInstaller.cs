using Reflex.Core;
using UnityEngine;

public class GameplaySceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private AudioService audioService;
    [SerializeField] private ViewUIManager viewUiManager;
    [SerializeField] private WorldSettings worldSettings;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(audioService);
        containerBuilder.AddSingleton(viewUiManager);
        containerBuilder.AddSingleton(worldSettings);
    }

}
