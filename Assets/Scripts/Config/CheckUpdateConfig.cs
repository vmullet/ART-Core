using UnityEngine;

[System.Serializable]
public class CheckUpdateConfig {

    [SerializeField]
    private bool checkUpdateAtStartup;
    [SerializeField]
    private string checkUpdateUrl;

    public bool CheckUpdateAtStartup => checkUpdateAtStartup;
    public string CheckUpdateUrl => checkUpdateUrl;
	
}
