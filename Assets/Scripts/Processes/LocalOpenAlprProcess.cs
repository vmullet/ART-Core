using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LocalOpenAlprProcess : MonoBehaviour,IProcess<byte[],LocalOpenAlprResult>,ILoggable {

    private LocalOpenAlprConfig localOpenAlprConfig;
    private OpenAlprAnim alprAnim;
    private LocalOpenAlprResult alprResult;
    private float executionTime;
    private bool isRunning = false;

    [SerializeField]
    private GUIUpdater guiUpdater;

    public bool IsDone => !isRunning;

    public LocalOpenAlprResult Result => alprResult;

    public float ExecutionTime => executionTime;

    private void Awake()
    {
        alprAnim = GetComponent<OpenAlprAnim>();
    }

    private void Start()
    {
        InitProcess();
    }


    public void StartProcess(byte[] input)
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(Process(input));
        }
        else
        {
            AppManager.System.ShowMessage("Alpr script is already running");
        }
    }

    public void StopProcess()
    {
        StopAllCoroutines();
    }

    public void ResetProcess()
    {
        alprResult = null;
    }

    public IEnumerator Process(byte[] picture)
    {
        float start = Time.realtimeSinceStartup;
        alprAnim.StartAnim();
        WWWForm form = new WWWForm();
        form.AddField("region", localOpenAlprConfig.Region);
        form.AddBinaryData("file", picture,"file.jpg","image/jpeg");
        using (UnityWebRequest www = UnityWebRequest.Post(GetUrl(), form))
        {
            yield return www.SendWebRequest();
            if (!www.isNetworkError || !www.isHttpError)
            {
                alprResult = LocalOpenAlprResult.CreateFromJson(www.downloadHandler.text);
            }
            else
            {
                AppManager.System.ShowMessage("OpenAlpr server error");
            }
        }
        alprAnim.StopAnim();
        yield return new WaitUntil(() => alprAnim.IsDone);
        isRunning = false;
        executionTime = Time.realtimeSinceStartup - start;
        yield return null;
    }

    private void InitProcess()
    {
        isRunning = false;
        localOpenAlprConfig = AppManager.Config.LocalOpenAlprConfig;
    }

    private string GetUrl()
    {
        return localOpenAlprConfig.Region == "eu" ? localOpenAlprConfig.Url : "http://79.137.72.55:5000/v3/api/detection";
    }

    public void SwitchRegion()
    {
        if (localOpenAlprConfig.Region == "eu")
            localOpenAlprConfig.Region = "gb";
        else
            localOpenAlprConfig.Region = "eu";
    }

    public string ToLogString()
    {
        return "Alpr : " + executionTime.ToString("n3") + " seconds";
    }

    public LogData ToLogData()
    {
        return new LogData("OpenAlpr " + localOpenAlprConfig.Region.ToUpper(), executionTime.ToString("n3") + " seconds");
    }
}
