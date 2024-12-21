using Reflex.Core;
using Trisibo;
using UnityEngine;
using UnityEngine.SceneManagement;
using Reflex.Extensions;

public class BootSceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private SceneField mainMenuScene;
    void Start()
    {
        StartCoroutine(sceneLoader.LoadScene(mainMenuScene.BuildIndex));
    }

}
