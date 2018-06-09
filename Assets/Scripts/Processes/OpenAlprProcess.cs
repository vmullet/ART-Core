using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAlprProcess : MonoBehaviour,IProcess<byte[],string>,ILoggable {

    private OpenAlprConfig openAlprConfig;
    private OpenAlprAnim alprAnim;
    private OpenAlprResult alprResult;
    private float executionTime;
    private bool isRunning = false;

    [SerializeField]
    private GUIUpdater guiUpdater;

    public bool IsDone => !isRunning;

    public string Result => alprResult != null ? alprResult.Plate : string.Empty;

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
        form.AddField("region", openAlprConfig.Region);
        form.AddBinaryData("file", picture,"file.jpg","image/jpeg");
        using (UnityWebRequest www = UnityWebRequest.Post(GetUrl(), form))
        {
            yield return www.SendWebRequest();
            if (!www.isNetworkError || !www.isHttpError)
            {
                alprResult = OpenAlprResult.CreateFromJson(www.downloadHandler.text);
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
        openAlprConfig = AppManager.Config.OpenAlprWS;
    }

    private string GetUrl()
    {
        return openAlprConfig.Region == "eu" ? openAlprConfig.Url : "http://79.137.72.55:5000/v3/api/detection";
    }

    public void SwitchRegion()
    {
        if (openAlprConfig.Region == "eu")
            openAlprConfig.Region = "gb";
        else
            openAlprConfig.Region = "eu";
    }

    public string ToLogString()
    {
        return "Alpr : " + executionTime.ToString("n3") + " seconds";
    }

    public LogData ToLogData()
    {
        return new LogData("OpenAlpr " + openAlprConfig.Region.ToUpper(), executionTime.ToString("n3") + " seconds");
    }
}
