using UnityEngine;

[System.Serializable]
public class OpenAlprResult {

    [SerializeField]
    private string result;

    public string Plate => result;

    public static OpenAlprResult CreateFromJson(string json)
    {
        return JsonUtility.FromJson<OpenAlprResult>(json);
    }

}
