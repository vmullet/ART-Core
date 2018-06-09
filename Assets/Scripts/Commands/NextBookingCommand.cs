﻿using UnityEngine;

[System.Serializable]
public class NextBookingCommand : ICommand
{
    [SerializeField]
    private ResultPanel bookingPanel;
    [SerializeField]
    private string commandName;

    public string Name => commandName;

    public void Execute()
    {
        bookingPanel.ShowNextBooking();
    }
}