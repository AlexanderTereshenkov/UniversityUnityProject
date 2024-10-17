using Reflex.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplaySceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private AudioService audioService;
    [SerializeField] private ViewUIManager viewUiManager;
    [SerializeField] private WorldSettings worldSettings;
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private Player player;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(audioService);
        containerBuilder.AddSingleton(viewUiManager);
        containerBuilder.AddSingleton(worldSettings);
        containerBuilder.AddSingleton(inputAction);
        containerBuilder.AddSingleton(player);
    }

}
