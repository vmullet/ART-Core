using UnityEngine;
using System;

public class SystemEditor : SystemBase
{
    private SystemEditor() { }

    public override string Date(string format) => DateTime.Now.ToString(format);

    public override void ShowMessage(string message)
    {
        Debug.Log(message);
    }
}
