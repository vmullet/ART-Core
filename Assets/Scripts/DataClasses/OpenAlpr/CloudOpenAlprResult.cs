using UnityEngine;

[System.Serializable]
public class CloudOpenAlprResult {

    [SerializeField]
    private string plate;

    public string Plate => plate;

    public static CloudOpenAlprResult CreateFromJson(string json)
    {
        return JsonUtility.FromJson<CloudOpenAlprResult>(json);
    }
	
}
