using System.Collections;
using UnityEngine;

public class ProcessControler : MonoBehaviour
{
    [SerializeField]
    private ScreenshotProcess screenshotProcess;
    [SerializeField]
    private LocalOpenAlprProcess localAlprProcess; // Plate Recognizer 1
    [SerializeField]
    private CloudOpenAlprProcess cloudAlprProcess; // Plate Recognizer 2
    [SerializeField]
    private InternetCheckProcess internetCheck;
    [SerializeField]
    private ResultPanel resultPanel;
    [SerializeField]
    private GUIUpdater guiUpdater;
    [SerializeField]
    private RecognizeType recognizeType;

    private bool isRunning = false;
    private PlateRecognizerProcess plateRecognizer; // The chosen Plate Recognizer

    private void Start()
    {
        isRunning = false;
        switch (recognizeType)
        {
            case RecognizeType.CLOUD_OPENALPR:
                plateRecognizer = cloudAlprProcess;
                break;
            case RecognizeType.LOCAL_OPENALPR:
                plateRecognizer = localAlprProcess;
                break;
            default:
                plateRecognizer = null;
                break;
        }
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
        resultPanel.ResetPanel();
        screenshotProcess.StartProcess();
        yield return new WaitUntil(() => screenshotProcess.IsDone);
        plateRecognizer.StartProcess(screenshotProcess.Result);
        yield return new WaitUntil(() => plateRecognizer.IsDone);
        if (plateRecognizer.Result != string.Empty)
        {
            resultPanel.ShowPlate(plateRecognizer.Result);
            // Add any post treatment there
            WriteLogs();
        }
        else
        {
            resultPanel.ShowPlateNotFound();
        }
        isRunning = false;
    }

    private void ResetAllProcess()
    {
        screenshotProcess.ResetProcess();
        plateRecognizer.ResetProcess();
    }

    private void ShowExecutionTime()
    {
        float total = plateRecognizer.ExecutionTime;
        guiUpdater.ShowTotalExecutionTime("Alpr : " + plateRecognizer.ExecutionTime.ToString("n3") + " sec\n"
            + "Total : " + total.ToString("n3") + " sec");
    }

    
    private void WriteLogs()
    {
        string picturePath = LogWriter.WritePicture(screenshotProcess.Result);
        resultPanel.ShowPicture(picturePath);

        float totalTime = plateRecognizer.ExecutionTime;
        LogRecord record = LogRecord.CreateRecord(new LogData(plateRecognizer.Result), 
                                                  plateRecognizer.ToLogData(),
                                                  new LogData("Total", totalTime.ToString("n3") + " seconds"),
                                                  GPS.ToLogData(),
                                                  new LogData("Picture",picturePath,true)
                                                  );
        LogWriter.WriteRecord(record);
    }

}
