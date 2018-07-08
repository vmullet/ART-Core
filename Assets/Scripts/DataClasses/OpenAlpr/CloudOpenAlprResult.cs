using UnityEngine;

[System.Serializable]
public class CloudOpenAlprResult {

    [SerializeField]
    private string plate;

    public string Plate => plate;
	
}


public class CloudOpenAlprResponse
{
    [SerializeField]
    private CloudOpenAlprResult[] results;

    public string BestPlate => results != null && results.Length > 0 ? results[0].Plate : string.Empty;

    public static CloudOpenAlprResponse CreateFromJson(string json)
    {
        return JsonUtility.FromJson<CloudOpenAlprResponse>(json);
    }
}
