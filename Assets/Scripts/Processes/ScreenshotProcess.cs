using System.Collections;
using UnityEngine;

public class ScreenshotProcess : MonoBehaviour,IProcess<string,byte[]> {

    private byte[] screenByte;
    private bool isRunning;
    private float executionTime;

    [SerializeField]
    private CameraStream cameraStream;

    public bool IsDone => !isRunning;

    public float ExecutionTime => executionTime;

    public byte[] Result => screenByte;

    private void Start()
    {
        isRunning = false;
    }

    public void StartProcess(string arg = null)
    {
        if (!isRunning)
        {
            StartCoroutine(AsyncScreenShot());
        }
    }

    private IEnumerator AsyncScreenShot()
    {
        float start = Time.realtimeSinceStartup;
        isRunning = true;
        yield return new WaitForEndOfFrame();
        if (cameraStream.CameraTexture != null)
        {
            int width = cameraStream.CameraTexture.width / 3;
            int height = cameraStream.CameraTexture.height / 3;
            Texture2D screenShot = new Texture2D(width, height);
            Color[] pixels = cameraStream.CameraTexture.GetPixels(width, height, width, height);
#if UNITY_VUZIX
            if (cameraStream.Orientation == -180)
                screenShot.SetPixels(ReversePixels(pixels));
            else
                screenShot.SetPixels(pixels);
#else
            screenShot.SetPixels(pixels);
#endif
            screenShot.Apply();
            screenByte = screenShot.EncodeToJPG();
            Destroy(screenShot);
        }
        isRunning = false;
        executionTime = Time.realtimeSinceStartup - start;
        yield return null;
    }

    public void ResetProcess()
    {
        screenByte = null;
    }

    public void StopProcess()
    {
        StopAllCoroutines();
    }

    private Color[] ReversePixels(Color[] pixels)
    {
        int currentIndex = 0;
        Color[] inverted = new Color[pixels.Length];
        for (int i = pixels.Length - 1; i >= 0; i--)
        {
            inverted[currentIndex] = pixels[i];
            currentIndex++;
        }
        return inverted;
    }

}
