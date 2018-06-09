using UnityEngine;

[System.Serializable]
public class SwitchEuGbCommand : ICommand {

    [SerializeField]
    private OpenAlprProcess openAlprProcess;
    [SerializeField]
    private string commandName;

    public string Name => commandName;

    public void Execute()
    {
        openAlprProcess.SwitchRegion();
    }

}
