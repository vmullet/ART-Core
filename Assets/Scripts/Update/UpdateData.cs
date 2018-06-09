using UnityEngine;

[System.Serializable]
public class UpdateData {

    [SerializeField]
    private string appName;
    [SerializeField]
    private string version;
    [SerializeField]
    private string url;

    public string AppName => appName;
    public string Version => version;
    public string Url => url;

    public static UpdateData CreateFromJson(string json)
    {
        return JsonUtility.FromJson<UpdateData>(json);
    }

}
