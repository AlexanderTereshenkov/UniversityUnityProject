using Reflex.Attributes;
using Trisibo;
using UnityEngine;

public class MainMenuPage : MonoBehaviour
{
    [SerializeField] private SceneField gameplayScene;
    private SceneLoader _sceneLoader;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader; 
    }

    public void LoadGameplay()
    {
        StartCoroutine(_sceneLoader.LoadScene(gameplayScene.BuildIndex));
    }
}

