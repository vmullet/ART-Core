public interface IProcess<T,U> {

    void StartProcess(T input); // Start the process with input parameters (if needed)
    void StopProcess();
    void ResetProcess();
    bool IsDone { get; } // To confirm that the process has finished
    U Result { get; } // Get the ouput of the process
    float ExecutionTime { get; }
	
}
