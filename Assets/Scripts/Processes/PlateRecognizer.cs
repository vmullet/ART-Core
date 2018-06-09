using UnityEngine;

public abstract class PlateRecognizer : MonoBehaviour, IProcess<byte[], string>, ILoggable
{
    public abstract bool IsDone { get; }

    public abstract string Result { get; }

    public abstract float ExecutionTime { get; }

    public abstract void ResetProcess();

    public abstract void StartProcess(byte[] input);

    public abstract void StopProcess();

    public abstract LogData ToLogData();
}
