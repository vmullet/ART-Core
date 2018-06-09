using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

    protected static T instance;

    public static T Instance => instance;

    protected void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = FindObjectOfType<T>();
        DontDestroyOnLoad(gameObject);
    }

    
}
