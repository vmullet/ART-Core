using UnityEngine;

[System.Serializable]
public class LocalOpenAlprResult {

    [SerializeField]
    private string result;

    public string Plate => result;

    public static LocalOpenAlprResult CreateFromJson(string json)
    {
        return JsonUtility.FromJson<LocalOpenAlprResult>(json);
    }

}
