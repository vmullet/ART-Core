using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomTextAnim : MonoBehaviour,IAnim {

    private Text container;
    private bool isRunning;
    private bool isFinished;

    [SerializeField]
    private float animSpeed;
    [SerializeField]
    private int textLength;
    [SerializeField]
    private int randomSeed;
    [SerializeField]
    private int firstAscii;
    [SerializeField]
    private int lastAscii;

    const int START_ASCII = 33;
    const int END_ASCII = 95;
    const float DEFAULT_ANIM_SPEED = .1f;
    const int DEFAULT_TEXT_LENGTH = 5;

    public bool IsDone => isFinished;

    private void Awake()
    {
        container = GetComponent<Text>();
    }

    private void Start () {
        InitAnim();
        if (animSpeed <= 0)
            animSpeed = DEFAULT_ANIM_SPEED;

        if (textLength <= 0)
            textLength = DEFAULT_TEXT_LENGTH;

	}

    public void StartAnim()
    {
        if (!isRunning)
        {
            isRunning = true;
            isFinished = false;
            StartCoroutine(Anim());
        }   
    }

    public void StopAnim()
    {
        isRunning = false;
    }

    private void InitAnim()
    {
        isRunning = false;
        isFinished = true;
    }

    private void ResetAnim()
    {
        container.text = string.Empty;
    }

    private IEnumerator Anim()
    {
        System.Random r = new System.Random(randomSeed);
        string randomText = string.Empty;
        while (isRunning)
        {
            randomText = string.Empty;
            for (int i = 0; i < textLength; i++)
            {
                int unicode = r.Next(firstAscii,lastAscii);
                randomText += (char)unicode;
            }
            container.text = randomText;
            yield return new WaitForSeconds(animSpeed);
            container.text = string.Empty;
        }
        ResetAnim();
        isFinished = true;
        yield return null;
    }

    
}
