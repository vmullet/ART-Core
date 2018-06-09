public class LogRecord {

    private LogData[] logData;

    const string RECORD_SEPARATOR = "----------------------------------------------------------------------------";
    const string NEW_LINE = "\r\n";

    private LogRecord(LogData[] data)
    {
        logData = data;
    }

    public static LogRecord CreateRecord(params LogData[] data)
    {
        LogRecord record = new LogRecord(data);
        return record;
    }

    public string ToLogString()
    {
        string encloser = RECORD_SEPARATOR + NEW_LINE;
        string result = encloser + "[" + AppManager.System.Date("yyyy-MM-dd HH:mm:ss") + "] : ";

        for (int i = 0; i < logData.Length; i++)
            result += logData[i].ToLogString();

        return result;
    }
	
}
