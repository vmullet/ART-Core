public abstract class SystemBase : ISystem
{
    public static SystemBase Instance { get; }

    public abstract string Date(string format);

    public abstract void ShowMessage(string message);
}
