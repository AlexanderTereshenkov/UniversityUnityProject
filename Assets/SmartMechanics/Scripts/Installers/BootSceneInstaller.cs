using Reflex.Core;
using UnityEngine;

public class BootSceneInstaller : MonoBehaviour, IInstaller
{

    [SerializeField] private InputManager inputManager;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(inputManager);
    }
}
