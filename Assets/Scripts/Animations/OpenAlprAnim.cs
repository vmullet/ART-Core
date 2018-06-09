using UnityEngine;

public class OpenAlprAnim : MonoBehaviour , IAnim
{
    [SerializeField]
    private RandomTextAnim randomTextAnim;

    public bool IsDone => randomTextAnim.IsDone;

    public void StartAnim()
    {
        randomTextAnim.StartAnim();
    }

    public void StopAnim()
    {
       randomTextAnim.StopAnim();
    }
}