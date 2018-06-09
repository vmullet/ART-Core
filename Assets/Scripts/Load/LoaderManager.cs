using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoSingleton<LoaderManager> {

    private string sceneToLoad;
    private string currentScene;
    private FaderAnim fader;

    const string LOADING_SCREEN = "LoadingScreen";

    public string SceneToLoad => sceneToLoad;

	public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == sceneName)
            yield break;

        sceneToLoad = sceneName;

        fader = FindObjectOfType<FaderAnim>();

        if (fader != null)
        {
            yield return new WaitUntil(() => fader.IsDone);
            fader.FadeInMode = false;
            fader.StartAnim();
            yield return new WaitUntil(() => fader.IsDone);
            SceneManager.LoadScene(LOADING_SCREEN);
        }
        else
        {
            AppManager.System.ShowMessage("There's no fader in this scene");
        }
    }

    public void ResetVar()
    {
        currentScene = string.Empty;
        sceneToLoad = string.Empty;
    }


}
