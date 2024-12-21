using Reflex.Attributes;
using UnityEngine;

public class InitializeGameplay : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private SceneLoader _sceneLoader;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {

        // _sceneLoader.EnableDisableCamera(_camera, true);
        Debug.Log(_sceneLoader == null);
    }
}
