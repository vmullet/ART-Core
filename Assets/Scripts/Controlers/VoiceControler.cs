using System.Collections;
using UnityEngine;

public class VoiceControler : MonoBehaviour {

    private VoiceRecognizerVuzix voiceRecognizer;
    private CommandDatabase commandDatabase;

    [SerializeField]
    private float triggerVoiceRate;

    const float DEFAULT_TRIGGER_RATE = 10f;

    private void Awake()
    {
#if !UNITY_VUZIX
        Destroy(this);
#endif
        commandDatabase = GetComponent<CommandDatabase>();
    }

    private void Start () {
        if (triggerVoiceRate < 0)
            triggerVoiceRate = DEFAULT_TRIGGER_RATE;

        voiceRecognizer = SingletonFactory.Instance<VoiceRecognizerVuzix>();
        MapVoice(AppManager.Config.SpeechControls);
        voiceRecognizer.EnableReceiver(true);

        StartCoroutine(TriggerVoiceReceiver());
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
        voiceRecognizer.TriggerVoice(false);
        voiceRecognizer.Unregister();
    }

    private void MapVoice(SpeechControl[] speechControls)
    {
        for (int i = 0; i < speechControls.Length; i++)
        {
            SpeechControl current = speechControls[i];
            if (commandDatabase.ContainsKey(current.Command))
            {
                voiceRecognizer.AddSpeech(current);
            }
        }
    }

    private IEnumerator TriggerVoiceReceiver()
    {
        while (true)
        {
            voiceRecognizer.TriggerVoice(true);
            yield return new WaitForSeconds(triggerVoiceRate);
        }
    }

    public void ExecuteVoiceCommand(string command)
    {
        if (commandDatabase.ContainsKey(command))
        {
            commandDatabase[command].Execute();
        }
    }
}
