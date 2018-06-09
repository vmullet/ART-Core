using UnityEngine;

[System.Serializable]
public class SpeechControl {

    [SerializeField]
    private string speech;
    [SerializeField]
    private string command;

    public string Speech => speech;
    public string Command => command;

}
