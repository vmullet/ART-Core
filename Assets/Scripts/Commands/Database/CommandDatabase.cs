using System.Collections.Generic;
using UnityEngine;

public class CommandDatabase : MonoBehaviour {

    private Dictionary<string, ICommand> commandDatabase; // Dictionary of all commands based on their names

    #region LIST OF COMMANDS

    [SerializeField]
    private TakePhotoCommand takePhoto;
    [SerializeField]
    private PreviousHistoryCommand previousHistory;
    [SerializeField]
    private NextHistoryCommand nextHistory;
    [SerializeField]
    private QuitCommand quitCommand;

    #endregion

    public ICommand this[string command] => commandDatabase[command];

    private void Awake()
    {
        InitDatabase();
    }

    private void InitDatabase()
    {
        commandDatabase = new Dictionary<string, ICommand>
        {
            { takePhoto.Name, takePhoto },
            { previousHistory.Name, previousHistory },
            { nextHistory.Name, nextHistory },
            { quitCommand.Name, quitCommand }
        };
    }

    public bool ContainsKey(string key)
    {
        return commandDatabase.ContainsKey(key);
    }
}
