using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    private FaderAnim fadeAnim; // The fadeAnim in the current scene
    private bool isLoading;
    private AsyncOperation loadingProcess;


    [SerializeField]
    private Image progressBar;

    public bool IsSceneLoaded => !isLoading;
    public float Progress => loadingProcess.progress;

	void Start () {
        isLoading = false;
        fadeAnim = FindObjectOfType<FaderAnim>();
        StartCoroutine(AsyncLoad(LoaderManager.Instance.SceneToLoad));
	}

    private IEnumerator AsyncLoad(string sceneName)
    {
        isLoading = true;

        yield return new WaitForSeconds(1f);

        loadingProcess = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single);
        loadingProcess.allowSceneActivation = false;

        while (loadingProcess.progress < .9f || progressBar.fillAmount < 1f)
        {
            progressBar.fillAmount += .01f;
            yield return new WaitForSeconds(.01f);
        }

        fadeAnim.FadeInMode = false;
        fadeAnim.StartAnim();

        yield return new WaitUntil(() => fadeAnim.IsDone);

        loadingProcess.allowSceneActivation = true;
        yield return new WaitUntil(() => loadingProcess.isDone);

        LoaderManager.Instance.ResetVar();
        isLoading = false;
        yield return null;

    }

}
