using UnityEngine;

[System.Serializable]
public class TakePhotoCommand : ICommand {

    [SerializeField]
    private ProcessControler processControler;
    [SerializeField]
    private string commandName;

    public string Name => commandName;

    public void Execute()
    {
        processControler.StartAllProcess();
    }
}
