using System.Collections.Generic;
using UnityEngine;

public class CommandDatabase : MonoBehaviour {

    private Dictionary<string, ICommand> commandDatabase; // Dictionary of all commands based on their names

    #region LIST OF COMMANDS

    [SerializeField]
    private TakePhotoCommand takePhoto;
    [SerializeField]
    private PreviousBookingCommand previousBooking;
    [SerializeField]
    private NextBookingCommand nextBooking;
    [SerializeField]
    private SwitchEuGbCommand switchEuGb;
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
            { previousBooking.Name, previousBooking },
            { nextBooking.Name, nextBooking },
            { switchEuGb.Name, switchEuGb },
            { quitCommand.Name, quitCommand }
        };
    }

    public bool ContainsKey(string key)
    {
        return commandDatabase.ContainsKey(key);
    }
}
