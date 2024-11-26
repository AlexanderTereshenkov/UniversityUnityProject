using Reflex.Core;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneLoader : MonoBehaviour
{

    void Start()
    {
        var bootScene = SceneManager.GetActiveScene();
        var sessionScene = SceneManager.LoadScene("DemoPlayTest", new LoadSceneParameters(LoadSceneMode.Additive));
        ReflexSceneManager.OverrideSceneParentContainer(sessionScene, bootScene.GetSceneContainer());
    }

}
