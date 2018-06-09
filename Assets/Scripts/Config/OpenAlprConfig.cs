using System;
using UnityEngine;

[Serializable]
public class OpenAlprConfig {

    [SerializeField]
    private string url;
    [SerializeField]
    private string defaultRegion;

    public string Url => url;
    public string Region
    {
        get { return defaultRegion.ToLower(); }
        set { defaultRegion = value; }
    }

    public bool IsValid()
    {
        return url != string.Empty 
            && defaultRegion != string.Empty;
    }


}
