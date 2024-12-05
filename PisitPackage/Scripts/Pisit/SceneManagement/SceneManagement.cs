using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pisit.SceneManagement
{
    public class SceneManagement : MonoBehaviour
    {
        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine( LoadSceneAsyncCoroutine(sceneName) );
        }

        IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.
            Debug.Log("Loading scene: " + sceneName);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
