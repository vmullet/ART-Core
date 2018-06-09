using UnityEngine;

[System.Serializable]
public class CloudOpenAlprConfig {

    [SerializeField]
    private string url;
    [SerializeField]
    private string token;
    [SerializeField]
    private string region;

    public string Url => url;
    public string Token => token;
    public string Region
    {
        get { return region; }
        set { region = value; }
    }

    public bool IsValid()
    {
        return url != string.Empty
            && token != string.Empty
            && region != string.Empty;
    }

}
