using System;
using UnityEngine;

[Serializable]
public class LocalOpenAlprConfig {

    [SerializeField]
    private string url;
    [SerializeField]
    private string region;

    public string Url => url;
    public string Region
    {
        get { return region.ToLower(); }
        set { region = value; }
    }

    public bool IsValid()
    {
        return url != string.Empty 
            && region != string.Empty;
    }


}
