using UnityEngine;

[System.Serializable]
public class CloudOpenAlprResult {


    public static CloudOpenAlprResult CreateFromJson(string json)
    {
        return JsonUtility.FromJson<CloudOpenAlprResult>(json);
    }
	
}
