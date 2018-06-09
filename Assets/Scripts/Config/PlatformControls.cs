using UnityEngine;

[System.Serializable]
public class PlatformControl {

    [SerializeField]
    private string button;
    [SerializeField]
    private string command;

    public string Button => button;
    public string Command => command;
	
}
