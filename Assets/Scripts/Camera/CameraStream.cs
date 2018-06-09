using UnityEngine;
using UnityEngine.UI;

public class CameraStream : MonoBehaviour
{
    private bool isCamAvailable;
    private WebCamTexture backCamera;
    private int orientation;

    [SerializeField]
    private RawImage background;
    [SerializeField]
    private AspectRatioFitter ratioFitter;
    [SerializeField]
    private bool frontCamera;
    [SerializeField]
    private RectTransform visor;

    public WebCamTexture CameraTexture => backCamera;
    public RawImage ImageTexture => background;
    public int Orientation => orientation;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            AppManager.System.ShowMessage("No Camera available");
            isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i ++)
        {
            if (devices[i].isFrontFacing == frontCamera)
            {
                backCamera = new WebCamTexture(devices[i].name,Screen.width,Screen.height);
                break;
            }
        }

        if (backCamera == null)
        {
            AppManager.System.ShowMessage("No Front Camera available");
            return;
        }

        backCamera.Play();
        background.texture = backCamera;

        float ratio = backCamera.width / backCamera.height;
        ratioFitter.aspectRatio = ratio;

        isCamAvailable = true;
    }

    private void Update()
    {
        if (!isCamAvailable)
            return;

        float scaleY = backCamera.videoVerticallyMirrored ? -1.0f : 1.0f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        
        orientation = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

    }

    private void OnApplicationPause(bool pause)
    {
        if (!isCamAvailable)
            return;

        if (pause)
            backCamera.Pause();
        else
            backCamera.Play();
    }

    private void OnApplicationQuit()
    {
        if (!isCamAvailable)
            return;

        backCamera.Stop();
    }
}
