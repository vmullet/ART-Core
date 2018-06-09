using System.Collections;
using UnityEngine;

public class ProcessControler : MonoBehaviour
{
    [SerializeField]
    private ScreenshotProcess screenshotProcess;
    [SerializeField]
    private OpenAlprProcess openAlprProcess;
    [SerializeField]
    private InternetCheckProcess internetCheck;
    [SerializeField]
    private ResultPanel bookingPanel;
    [SerializeField]
    private GUIUpdater guiUpdater;

    private bool isRunning = false;

    private void Start()
    {
        isRunning = false;
        guiUpdater.ShowTotalExecutionTime(string.Empty);
    }

    public void StartAllProcess()
    {
        if (!isRunning)
        {
            if (internetCheck.Result)
            {
                guiUpdater.ShowTotalExecutionTime(string.Empty);
                ResetAllProcess();
                StartCoroutine(AsyncProcess());
            }
            else
            {
                AppManager.System.ShowMessage("You have no internet connection");
            }
        }
        else
        {
            AppManager.System.ShowMessage("The process is already running, please wait");
        }
    }

    private IEnumerator AsyncProcess()
    {
        isRunning = true;
        bookingPanel.ResetPanel();
        screenshotProcess.StartProcess();
        yield return new WaitUntil(() => screenshotProcess.IsDone);
        openAlprProcess.StartProcess(screenshotProcess.Result);
        yield return new WaitUntil(() => openAlprProcess.IsDone);
        if (openAlprProcess.Result != string.Empty)
        {
            bookingPanel.ShowPlate(openAlprProcess.Result);
        }
        else
        {
            bookingPanel.ShowPlateNotFound();
        }
        isRunning = false;
    }

    private void ResetAllProcess()
    {
        screenshotProcess.ResetProcess();
        openAlprProcess.ResetProcess();
    }

    private void ShowExecutionTime()
    {
        float total = openAlprProcess.ExecutionTime;
        AppManager.System.ShowMessage(openAlprProcess.ToLogString());
        guiUpdater.ShowTotalExecutionTime("Alpr : " + openAlprProcess.ExecutionTime.ToString("n3") + " sec\n"
            + "Total : " + total.ToString("n3") + " sec");
    }

    
    private void WriteLogs()
    {
        string picturePath = LogWriter.WritePicture(screenshotProcess.Result);

        float totalTime = openAlprProcess.ExecutionTime;
        LogRecord record = LogRecord.CreateRecord(new LogData(openAlprProcess.Result), 
                                                  openAlprProcess.ToLogData(),
                                                  new LogData("Total", totalTime.ToString("n3") + " seconds"),
                                                  GPS.ToLogData(),
                                                  new LogData("Picture",picturePath,true)
                                                  );
        LogWriter.WriteRecord(record);
    }

}
