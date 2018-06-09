using System;
using UnityEngine;

[Serializable]
public class AppConfig {

    [SerializeField]
    private string configVersion; // Useful to check that the config file is adapted to the version of the application
    [SerializeField]
    private CheckUpdateConfig checkUpdateConfig;
    [SerializeField]
    private LocalOpenAlprConfig localOpenAlprConfig;
    [SerializeField]
    private CloudOpenAlprConfig cloudOpenAlprConfig;
    [SerializeField]
    private PlatformControl[] vuzixControls;
    [SerializeField]
    private PlatformControl[] androidControls;
    [SerializeField]
    private SpeechControl[] speechControls;

    public string ConfigVersion => configVersion;
    public CheckUpdateConfig CheckUpdateConfig => checkUpdateConfig;
    public LocalOpenAlprConfig LocalOpenAlprConfig => localOpenAlprConfig;
    public CloudOpenAlprConfig CloudOpenAlprConfig => cloudOpenAlprConfig;
    public PlatformControl[] VuzixControls => vuzixControls;
    public PlatformControl[] AndroidControls => androidControls;
    public SpeechControl[] SpeechControls => speechControls;
    
    public static AppConfig LoadFromJSon(string json)
    {
        return JsonUtility.FromJson<AppConfig>(json);
    }

    public bool IsValid()
    {
        return configVersion == Application.version
        && (localOpenAlprConfig.IsValid()
        || cloudOpenAlprConfig.IsValid());
    }

}
