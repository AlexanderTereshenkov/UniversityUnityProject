using Reflex.Core;
using UnityEngine;

public class GameplaySceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private AudioService audioService;
    [SerializeField] private ViewUIManager viewUiManager;
    [SerializeField] private WorldSettings worldSettings;
    [SerializeField] private Player player;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(audioService)
            .AddSingleton(viewUiManager)
            .AddSingleton(worldSettings)
            .AddSingleton(player);
    }

}
