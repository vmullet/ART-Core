using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InternetCheckProcess : MonoBehaviour,IProcess<string,bool> {

    private bool hasInternet;
    private bool isRunning;
    private float executionTime;
    private InternetCheckAnim internetAnim;

    [SerializeField]
    private float checkRate;

    const string CHECK_INTERNET_URL = "http://www.google.com";
    const float DEFAULT_CHECK_INTERNET_RATE = 10f;

    public bool IsDone => !isRunning;

    public float ExecutionTime => executionTime;

    public bool Result => hasInternet;

    private void Awake()
    {
        internetAnim = GetComponent<InternetCheckAnim>();
    }

    private void Start()
    {
        isRunning = false;
        hasInternet = false;

        if (checkRate <= 0)
            checkRate = DEFAULT_CHECK_INTERNET_RATE;

        StartProcess();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            StopProcess();
        else
            StartProcess();
    }

    private void OnApplicationQuit()
    {
        StopProcess();
    }

    public void ResetProcess()
    {
        hasInternet = false;
    }

    public void StartProcess(string input = null)
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(CheckInternet());
        }
    }

    public void StopProcess()
    {
        StopAllCoroutines();
        isRunning = false;
        hasInternet = false;
    }

    private IEnumerator CheckInternet()
    {
        float start = Time.realtimeSinceStartup;
        while (isRunning)
        {
            internetAnim.StartAnim();
            using (UnityWebRequest www = UnityWebRequest.Get(CHECK_INTERNET_URL))
            {
                yield return www.SendWebRequest();
                hasInternet = !(www.isHttpError || www.isNetworkError);
            }
            internetAnim.StopAnim();
            yield return new WaitUntil(() => internetAnim.IsDone);
            internetAnim.ShowIcon(hasInternet);
            yield return new WaitForSeconds(checkRate);
        }
        executionTime = Time.realtimeSinceStartup - start;
    }

}
