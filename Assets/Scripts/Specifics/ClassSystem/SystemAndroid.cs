using UnityEngine;

public class SystemAndroid : SystemBase
{
    private const string JAVA_PACKAGE_CLASS = "com.unity.art.plugin.android.ArtSystem";

    private AndroidJavaObject androidSystem;

    #region SINGLETON

    private SystemAndroid()
    {
        androidSystem =  new AndroidJavaObject(JAVA_PACKAGE_CLASS);
    }

    #endregion

    #region FUNCTION NAMES

    const string FUNC_TOAST = "showMessage";
    const string FUNC_DATE = "getDate";

    #endregion

    public override string Date(string format) => androidSystem.Call<string>(FUNC_DATE, format);

    public override void ShowMessage(string message) { androidSystem.Call(FUNC_TOAST, message); }

}
