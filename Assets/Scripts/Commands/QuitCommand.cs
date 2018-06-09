using UnityEngine;

[System.Serializable]
public class QuitCommand : ICommand
{
    [SerializeField]
    private string commandName;

    public string Name => commandName;

    public void Execute()
    {
        Application.Quit();
    }
}
