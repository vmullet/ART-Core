using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.Networking;

public class AppManager : MonoSingleton<AppManager> {

    private AppConfig appConfig;
    private UpdateData updateData;
    private string configFolderPath;
    private string configFilePath;
    private SystemBase systemInfo;
    private ArtHistory artHistory;
    
    [SerializeField]
    private TextAsset defaultConfig;
    [SerializeField]
    private bool forceReset;
    
    #region PROPERTIES

    public static SystemBase System => instance.systemInfo;
    public static ArtHistory ArtHistory => instance.artHistory;
    public static AppConfig Config => instance.appConfig;
    public static UpdateData UpdateData => instance.updateData;

    #endregion

    #region CONSTANTS

    const string CONFIG_FOLDER = "config";
    const string CONFIG_FILENAME = "appConfig.json";
    const string CAMERA_SCENE = "CameraScene";
    const string CAMERA_SCENE_LIGHTUI = "CameraSceneLightUI";
    const string UPDATER_SCENE = "UpdaterScene";

    #endregion

    private void Start()
    {
        InitSingletons();
        LoadConfig();
        StartCameraScene();
        //StartCoroutine(CheckUpdate());
    }

    #region PRIVATE METHODS

    private void InitSingletons()
    {
        artHistory = SingletonFactory.Instance<ArtHistory>();

#if UNITY_EDITOR
        systemInfo = SingletonFactory.Instance<SystemEditor>();
#elif UNITY_ANDROID
        systemInfo = SingletonFactory.Instance<SystemAndroid>();
#endif

    }

    private void LoadConfig()
    {
        configFolderPath = Path.Combine(Application.persistentDataPath, CONFIG_FOLDER);
        configFilePath = Path.Combine(configFolderPath, CONFIG_FILENAME);
        bool isFileJson = true;

        if (!Directory.Exists(configFolderPath))
            Directory.CreateDirectory(configFolderPath);

        if (!File.Exists(configFilePath))
            File.WriteAllText(configFilePath, defaultConfig.text);
        else if (forceReset)
            ResetConfig();

        try
        {
            appConfig = AppConfig.LoadFromJSon(File.ReadAllText(configFilePath));
        }
        catch (ArgumentException)
        {
            isFileJson = false;
        }

        if (!isFileJson || !appConfig.IsValid())
        {
            ResetConfig();
            systemInfo.ShowMessage("Config Invalid, the config has been reset to default");
        }
    }

    private void ResetConfig()
    {
        File.Delete(configFilePath);
        File.WriteAllText(configFilePath, defaultConfig.text);
        appConfig = AppConfig.LoadFromJSon(defaultConfig.text);
    }

    private IEnumerator CheckUpdate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(""))
        {
            yield return www.SendWebRequest();
            if (!www.isHttpError && !www.isNetworkError)
            {
                updateData = UpdateData.CreateFromJson(www.downloadHandler.text);
                if (updateData.Version != Application.version)
                {
                    systemInfo.ShowMessage(string.Format("Une nouvelle mise à jour est disponible {0} {1}",updateData.Version, Application.version));
                    LoaderManager.Instance.LoadScene(UPDATER_SCENE);
                }
                else
                {
                    StartCameraScene();
                }
            }
            else
            {
                StartCameraScene();
            }
        }
        yield return null;
    }

    private void StartCameraScene()
    {
#if LIGHTUI
        LoaderManager.Instance.LoadScene(CAMERA_SCENE_LIGHTUI);
#else
        LoaderManager.Instance.LoadScene(CAMERA_SCENE);
#endif
    }

    #endregion
}
