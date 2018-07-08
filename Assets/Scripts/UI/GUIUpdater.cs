using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIUpdater : MonoBehaviour {

    #region FIELDS LIST

    [SerializeField]
    private Text dateText;
    [SerializeField]
    private Text batteryText;
    [SerializeField]
    private Text statTotalHistory;
    [SerializeField]
    private Text historyNotFound;
    [SerializeField]
    private Text totalExecutionTime;
    [SerializeField]
    private Text gpsText;
    [SerializeField]
    private Text devProdText;
    [SerializeField]
    private Text btnCloudLocal;
    [SerializeField]
    private Image batteryImage;
    [SerializeField]
    private float refreshOSRate;

    #endregion

    const string DATE_MASK = "dd/MM/yy HH:mm:ss";
    const float DEFAULT_REFRESH_OS_RATE = 10f;
    const float MAX_BATTERY = 100f;

    readonly Color COLOR_ORANGE = new Color(1f, .6f, 0f);

    private void Start () {
        if (refreshOSRate <= 0)
            refreshOSRate = DEFAULT_REFRESH_OS_RATE;

#if UNITY_VUZIX
        btnDevProdText.transform.parent.gameObject.SetActive(false);
#else
        devProdText.transform.parent.gameObject.SetActive(false);
#endif

        StartCoroutine(RefreshOSValues());
        StartCoroutine(RefreshDate());
	}

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine(RefreshOSValues());
            StartCoroutine(RefreshDate());
        }
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

    public void UpdateHistory()
    {
        statTotalHistory.text = AppManager.ArtHistory.CountHistory.ToString() + "\nVehicle";
        historyNotFound.text = "Unknown\n" + AppManager.ArtHistory.CountNotFound;
    }

    public void ShowTotalExecutionTime(string executionTime)
    {
        totalExecutionTime.text = executionTime;
    }
	
    private IEnumerator RefreshDate()
    {
        while (true)
        {
            dateText.text = AppManager.System.Date(DATE_MASK);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator RefreshOSValues()
    {
        while(true)
        {
            int batteryPercent = (int) (SystemInfo.batteryLevel * 100f);
            batteryText.text = batteryPercent + "%";
            batteryText.color = FindBatteryTextColor(batteryPercent);
            batteryImage.fillAmount = SystemInfo.batteryLevel;
            yield return new WaitForSeconds(refreshOSRate);
        }
    }

    private Color FindBatteryTextColor(int batteryValue)
    {
        if (batteryValue < 25)
            return Color.red;
        else if (batteryValue < 50)
            return COLOR_ORANGE;
        else if (batteryValue < 75)
            return Color.yellow;
        else
            return Color.green;
    }

    public void SetRecognizeMode(RecognizeType mode)
    {
        if (mode == RecognizeType.CLOUD_OPENALPR)
            btnCloudLocal.text = "CLOUD";
        else
            btnCloudLocal.text = "LOCAL";
    }
}
