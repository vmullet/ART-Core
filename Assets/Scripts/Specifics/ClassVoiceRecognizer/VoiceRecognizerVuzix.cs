using UnityEngine;

public class VoiceRecognizerVuzix {

    private AndroidJavaObject voiceCommandReceiver;

    const string PKG_UNITY_PLAYER = "com.unity3d.player.UnityPlayer";
    const string PKG_VOICE_RECEIVER = "com.unity.art.plugin.android.VoiceCommandReceiver";

    const string PROP_CURRENT_ACTIVITY = "currentActivity";
    
    const string FUNC_ENABLE_RECEIVER = "enableReceiver";
    const string FUNC_ADD_SPEECH = "addSpeech";
    const string FUNC_TRIGGER_VOICE = "triggerVoice";
    const string FUNC_UNREGISTER = "unregister";


    private VoiceRecognizerVuzix()
    {
        using (AndroidJavaClass javaUnityPlayer = new AndroidJavaClass(PKG_UNITY_PLAYER))
        {
            AndroidJavaObject mainActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>(PROP_CURRENT_ACTIVITY);
            voiceCommandReceiver = new AndroidJavaObject(PKG_VOICE_RECEIVER, mainActivity);
        }
    }

    public void EnableReceiver(bool enable)
    {
        voiceCommandReceiver.Call(FUNC_ENABLE_RECEIVER, enable);
    }

    public void AddSpeech(SpeechControl speechControl)
    {
        voiceCommandReceiver.Call(FUNC_ADD_SPEECH, speechControl.Speech, speechControl.Command);
    }

    public void TriggerVoice(bool trigger)
    {
        voiceCommandReceiver.Call(FUNC_TRIGGER_VOICE, trigger);
    }

    public void Unregister()
    {
        voiceCommandReceiver.Call(FUNC_UNREGISTER);
    }

}
