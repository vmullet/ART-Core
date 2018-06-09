using System.Collections.Generic;
using UnityEngine;

public class ButtonControler : MonoBehaviour {

    private CommandDatabase commandDatabase; // Dictionary of all commands based on their names
    private Dictionary<string, string> buttonMapper; // Mapper built from config file

    private void Awake()
    {
        commandDatabase = GetComponent<CommandDatabase>();
    }

    private void Start()
    {
        InitMapper();
    }

    private void InitMapper()
    {
        buttonMapper = new Dictionary<string, string>();
#if UNITY_ANDROID
#if UNITY_VUZIX
        MapControls(AppManager.Config.VuzixControls);
#else
        MapControls(AppManager.Config.AndroidControls);
#endif
#endif
    }

    private void MapControls(PlatformControl[] configControls)
    {
        for (int i = 0; i < configControls.Length; i++)
        {
            PlatformControl current = configControls[i];
            if (!buttonMapper.ContainsKey(current.Button)
                && commandDatabase.ContainsKey(current.Command))
            {
                buttonMapper.Add(current.Button, current.Command);
            }
        }
    }

    public void ExecuteButtonCommand(string button)
    {
        if (buttonMapper.ContainsKey(button))
        {
            string command = buttonMapper[button];
            commandDatabase[command].Execute();
        }
    }
}
