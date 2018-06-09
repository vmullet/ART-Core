using UnityEngine;

[System.Serializable]
public class PreviousHistoryCommand : ICommand {

    [SerializeField]
    private ResultPanel resultPanel;
    [SerializeField]
    private string commandName;

    public string Name => commandName;

    public void Execute()
    {
        resultPanel.ShowPreviousHistory();
    }

}
