using System;
using UnityEngine;

[Serializable]
public class AppConfig
{
    [SerializeField]
    private OpenAlprConfig openAlprConfig;
    [SerializeField]
    private PlatformControl[] vuzixControls;
    [SerializeField]
    private PlatformControl[] androidControls;
    [SerializeField]
    private SpeechControl[] speechControls;

    public OpenAlprConfig OpenAlprWS => openAlprConfig;
    public PlatformControl[] VuzixControls => vuzixControls;
    public PlatformControl[] AndroidControls => androidControls;
    public SpeechControl[] SpeechControls => speechControls;
    
    public static AppConfig LoadFromJSon(string json)
    {
        return JsonUtility.FromJson<AppConfig>(json);
    }

    public bool IsValid()
    {
        return openAlprConfig.IsValid();
    }

}
