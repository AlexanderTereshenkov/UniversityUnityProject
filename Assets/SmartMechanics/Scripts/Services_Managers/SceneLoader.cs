using Reflex.Core;
using Reflex.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _isSceneLoaded;
    private int _currentSceneIndex;

    public void LoadScene(int buildIndex)
    {
        StartCoroutine(LoadSceneAsync(buildIndex));
    }

    private IEnumerator LoadSceneAsync(int buildIndex)
    {
        if (_isSceneLoaded)
        {
            AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(_currentSceneIndex);
            yield return unloadAsync;
            /*
            while (!unloadAsync.isDone)
            {
                yield return null;
            }
            */
        }
        var bootScene = SceneManager.GetSceneByName("BootScene");
        var sessionScene = SceneManager.LoadScene(buildIndex, new LoadSceneParameters(LoadSceneMode.Additive));
        ReflexSceneManager.OverrideSceneParentContainer(scene: sessionScene, parent: bootScene.GetSceneContainer());
        _currentSceneIndex = buildIndex;
        _isSceneLoaded = true;
        //make scene active but in next frame, FIX IT
        yield return null;
        SceneManager.SetActiveScene(sessionScene);
        Time.timeScale = 1;
    }

}
