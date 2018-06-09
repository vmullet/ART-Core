using System.Collections;
using UnityEngine;

public class GPS : MonoSingleton<GPS> {

    private float latitude;
    private float longitude;
    private int maxWait;
    private GPSStatus status;

    [SerializeField]
    private float refreshRateGps;

    const int DEFAULT_TIMEOUT = 10;
    const string LAT_LONG_SEPARATOR = " ; ";

    public static float Longitude => instance.longitude;
    public static float Latitude => instance.latitude;
    public static GPSStatus Status => instance.status;

    private void Start()
    {
        status = GPSStatus.STOPPED;
#if UNITY_EDITOR
        status = GPSStatus.STOPPED;
#elif UNITY_ANDROID
        Initialize();
#endif
    }

    private void OnApplicationPause(bool pause)
    {
#if UNITY_EDITOR
        status = GPSStatus.STOPPED;
#elif UNITY_ANDROID
        if (status != GPSStatus.FAILED)
        {
            if (pause)
                StopGPS();
            else
                Initialize();
        } 
#endif
    }

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        status = GPSStatus.STOPPED;
#elif UNITY_ANDROID
        StopGPS();
#endif
    }

    private void Initialize()
    {
        maxWait = DEFAULT_TIMEOUT;
        longitude = 0;
        latitude = 0;
        StartCoroutine(StartGps());
    }

    private IEnumerator StartGps()
    {
        if (status == GPSStatus.STOPPED)
        {
            status = GPSStatus.INITIALIZING;

            if (!Input.location.isEnabledByUser)
            {
                AppManager.System.ShowMessage("GPS is not enabled on your device");
                status = GPSStatus.FAILED;
                yield break;
            }

            Input.location.Start();
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait <= 0)
            {
                //AppManager.System.ShowMessage("Timed out starting GPS");
                status = GPSStatus.FAILED;
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                AppManager.System.ShowMessage("Unable to determine device location");
                status = GPSStatus.FAILED;
                yield break;
            }

            status = GPSStatus.RUNNING;
            StartCoroutine(UpdateGps());

        }
        yield return null;
    }

    private IEnumerator UpdateGps()
    {
        while(true)
        {
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
            yield return new WaitForSeconds(refreshRateGps);
        }
    }

    private void StopGPS()
    {
        StopAllCoroutines();
        Input.location.Stop();
        status = GPSStatus.STOPPED;
    }

    public static LogData ToLogData()
    {
        return instance.status == GPSStatus.RUNNING ?
            new LogData("GPS",instance.latitude + LAT_LONG_SEPARATOR + instance.longitude,true) : new LogData("NO GPS",true);
    }

}
