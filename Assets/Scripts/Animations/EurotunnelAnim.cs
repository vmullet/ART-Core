using UnityEngine;

public class EurotunnelAnim : MonoBehaviour,IAnim {

    private bool isRunning;
    [SerializeField]
    private RandomTextAnim[] textAnimations;

    void Start()
    {
        isRunning = false;
    }

    public bool IsDone => IsAllFinished();

    public void StartAnim()
    {
        if (!isRunning)
        {
            isRunning = true;
            for (int i = 0; i < textAnimations.Length; i++)
            {
                textAnimations[i].StartAnim();
            }
        }
    }

    public void StopAnim()
    {
        for (int i = 0; i < textAnimations.Length; i++)
        {
            textAnimations[i].StopAnim();
        }
        isRunning = false;
    }

    private bool IsAllFinished()
    {
        for (int i = 0; i < textAnimations.Length; i++)
        {
            if (textAnimations[i].IsDone)
                return false;
        }
        return true;
    }

    
}
