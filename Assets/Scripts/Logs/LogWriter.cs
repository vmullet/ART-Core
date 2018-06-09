using System.IO;
using UnityEngine;

public static class LogWriter {

    const string NEW_LINE = "\r\n";
    const string DATA_SEPARATOR = " - ";
    const string LOGS_FOLDER_NAME = "logs";
    const string LOGS_EXTENSION = ".log";
    const string PICTURES_FOLDER_NAME = "pictures";
    const string PICTURES_EXTENSION = ".jpg";

    static readonly string LOGS_FILES_PATH = Path.Combine(Application.persistentDataPath, LOGS_FOLDER_NAME);
    static readonly string LOGS_PICTURES_PATH = Path.Combine(Application.persistentDataPath, PICTURES_FOLDER_NAME);
    static readonly string CURRENT_LOG_PATH = Path.Combine(LOGS_FILES_PATH, AppManager.System.Date("yyyyMMdd") + LOGS_EXTENSION);

    public static string WriteRecord(LogRecord record)
    {
        if (!Directory.Exists(LOGS_FILES_PATH))
            Directory.CreateDirectory(LOGS_FILES_PATH);

        if (!File.Exists(CURRENT_LOG_PATH))
            File.WriteAllText(CURRENT_LOG_PATH, record.ToLogString());
        else
            File.AppendAllText(CURRENT_LOG_PATH, record.ToLogString());

        return CURRENT_LOG_PATH;

    }

    public static string WritePicture(byte[] picture)
    {
        if (!Directory.Exists(LOGS_PICTURES_PATH))
            Directory.CreateDirectory(LOGS_PICTURES_PATH);

        string picturePath = Path.Combine(LOGS_PICTURES_PATH, AppManager.System.Date("yyyyMMdd_HHmmss") + PICTURES_EXTENSION);
        File.WriteAllBytes(picturePath, picture);

        return picturePath;

    }

}
