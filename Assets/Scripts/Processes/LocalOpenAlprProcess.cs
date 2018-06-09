using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LocalOpenAlprProcess : PlateRecognizer {

    private LocalOpenAlprConfig localOpenAlprConfig;
    private OpenAlprAnim alprAnim;
    private LocalOpenAlprResult alprResult;
    private float executionTime;
    private bool isRunning = false;

    public override bool IsDone => !isRunning;

    public override string Result => alprResult.Plate;

    public override float ExecutionTime => executionTime;

    private void Awake()
    {
        alprAnim = GetComponent<OpenAlprAnim>();
    }

    private void Start()
    {
        InitProcess();
    }


    public override void StartProcess(byte[] input)
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

    public override void StopProcess()
    {
        StopAllCoroutines();
    }

    public override void ResetProcess()
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
        using (UnityWebRequest www = UnityWebRequest.Post(localOpenAlprConfig.Url, form))
        {
            yield return www.SendWebRequest();
            if (!www.isNetworkError || !www.isHttpError)
            {
                alprResult = LocalOpenAlprResult.CreateFromJson(www.downloadHandler.text);
            }
            else
            {
                AppManager.System.ShowMessage("Local OpenAlpr server error");
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

    public string ToLogString()
    {
        return "Alpr : " + executionTime.ToString("n3") + " seconds";
    }

    public override LogData ToLogData()
    {
        return new LogData("OpenAlpr " + localOpenAlprConfig.Region.ToUpper(), executionTime.ToString("n3") + " seconds");
    }
}
