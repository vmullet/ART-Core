public class LogData {

    private string label;
    private string value;

    private const string LABEL_VALUE_SEPARATOR = " : ";
    private const string NEW_LINE = "\r\n";
    private const string DATA_SEPARATOR = " - ";

    public LogData(string label,string value)
    {
        this.label = label;
        this.value = value + DATA_SEPARATOR;
    }

    public LogData(string value)
    {
        label = string.Empty;
        this.value = value + DATA_SEPARATOR;
    }

    public LogData(string label,string value,bool newLine = false)
    {
        this.label = label;
        this.value = newLine ? value + DATA_SEPARATOR + NEW_LINE : value + DATA_SEPARATOR;
    }

    public LogData(string value,bool newLine = false)
    {
        label = string.Empty;
        this.value = newLine ? value + DATA_SEPARATOR + NEW_LINE : value + DATA_SEPARATOR;
    }

    public string ToLogString()
    {
        return label == string.Empty ? value : label + LABEL_VALUE_SEPARATOR + value;
    }

}
