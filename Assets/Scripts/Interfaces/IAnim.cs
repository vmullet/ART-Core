public interface IAnim {

    void StartAnim();
    void StopAnim();
    bool IsDone { get; } // To confirm that the animation is completely finished
	
}
