using Reflex.Core;
using UnityEngine;

public class BootSceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private SceneLoader sceneLoader;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(inputManager);
        containerBuilder.AddSingleton(sceneLoader);
    }
}
