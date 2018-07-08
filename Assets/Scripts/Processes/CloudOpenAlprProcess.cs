using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CloudOpenAlprProcess : PlateRecognizerProcess {

    private CloudOpenAlprConfig cloudOpenAlprConfig;
    private OpenAlprAnim alprAnim;
    private CloudOpenAlprResponse alprResponse;
    private float executionTime;
    private bool isRunning = false;

    #region CONSTANTES
    const string IMAGE_PARAMETER = "image";
    #endregion

    public override bool IsDone => !isRunning;

    public override string Result => alprResponse.BestPlate;

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

    public IEnumerator Process(byte[] picture)
    {
        float start = Time.realtimeSinceStartup;
        alprAnim.StartAnim();
        WWWForm form = new WWWForm();
        form.AddBinaryData(IMAGE_PARAMETER,picture);
        using (UnityWebRequest www = UnityWebRequest.Post(GetUrl(), form))
        {
            yield return www.SendWebRequest();
            if (!www.isNetworkError || !www.isHttpError)
            {
                alprResponse = CloudOpenAlprResponse.CreateFromJson(www.downloadHandler.text);
            }
            else
            {
                AppManager.System.ShowMessage("Cloud OpenAlpr server error");
            }
        }
        alprAnim.StopAnim();
        yield return new WaitUntil(() => alprAnim.IsDone);
        isRunning = false;
        executionTime = Time.realtimeSinceStartup - start;
        yield return null;
    }

        public override void StopProcess()
    {
        StopAllCoroutines();
    }

    public override void ResetProcess()
    {
        alprResponse = null;
    }

    private void InitProcess()
    {
        isRunning = false;
        cloudOpenAlprConfig = AppManager.Config.CloudOpenAlprConfig;
    }

    private string GetUrl()
    {
        return string.Format(cloudOpenAlprConfig.Url,cloudOpenAlprConfig.Region,cloudOpenAlprConfig.Token);
    }

    public override LogData ToLogData()
    {
        return new LogData("OpenAlpr " + cloudOpenAlprConfig.Region.ToUpper(), executionTime.ToString("n3") + " seconds");
    }
}
