using UnityEngine;
using System.Collections;

public class CloudOpenAlprProcess : MonoBehaviour,IProcess<byte[], CloudOpenAlprResult>, ILoggable
{

    private CloudOpenAlprConfig cloudOpenAlprConfig;
    private OpenAlprAnim alprAnim;
    private CloudOpenAlprResult alprResult;

    private float executionTime;
    private bool isRunning = false;

    public bool IsDone => !isRunning;

    public CloudOpenAlprResult Result => alprResult;

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

    public IEnumerator Process(byte[] picture)
    {
        yield return null;
    }

        public void StopProcess()
    {
        StopAllCoroutines();
    }

    public void ResetProcess()
    {
        alprResult = null;
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

    public LogData ToLogData()
    {
        return new LogData("OpenAlpr " + cloudOpenAlprConfig.Region.ToUpper(), executionTime.ToString("n3") + " seconds");
    }
}
