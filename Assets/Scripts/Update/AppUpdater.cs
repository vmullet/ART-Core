using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

public class AppUpdater : MonoBehaviour {

    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Text progressText;
    [SerializeField]
    private Text updateStatus;

    private UnityWebRequest www;
    private string updateFolderPath;
    private string updateFilePath;
    private bool isSuccess;
    private bool isDone;

    const string UPDATE_FOLDER = "update";
    const string UPDATE_FILE = "artUpdate.apk";


    private void Start() {
        isDone = false;
        isSuccess = false;
        updateFolderPath = Path.Combine(Application.persistentDataPath, UPDATE_FOLDER);
        updateFilePath = Path.Combine(updateFolderPath, UPDATE_FILE);
        updateStatus.text = string.Format("Téléchargement de la version {0}", AppManager.UpdateData.Version);

        StartCoroutine(DownloadUpdate());
        StartCoroutine(ShowProgress());
	}

    private IEnumerator DownloadUpdate()
    {
        if (!Directory.Exists(updateFolderPath))
            Directory.CreateDirectory(updateFolderPath);

        using (www = UnityWebRequest.Get(AppManager.UpdateData.Url))
        {
            www.downloadHandler = new DownloadHandlerFile(updateFilePath);
            yield return www.SendWebRequest();
            isDone = true;
            if (!www.isHttpError && !www.isNetworkError)
            {
                isSuccess = true;
                AppManager.System.ShowMessage("Mise à jour téléchargée, l'application va être fermée et mise à jour");
                yield return new WaitForSeconds(3f);
                Application.Quit();
            }
            else
            {
                AppManager.System.ShowMessage("Echec lors du téléchargement de la mise à jour");
            }
        }
        yield return null;
    }

    private IEnumerator ShowProgress()
    {
        while (!isDone)
        {
            progressBar.fillAmount = www.downloadProgress;
            progressText.text = string.Format("{0} %", progressBar.fillAmount * 100);
            yield return new WaitForSeconds(.1f);
        }
        yield return null;
    }

    private void OnApplicationQuit()
    {
        if (isSuccess)
        {
            Application.OpenURL(updateFilePath);
        }
    }
}
