using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InternetCheckAnim : MonoBehaviour,IAnim {

    private Image[] iconFragments;
    private bool isRunning;
    private bool isFinished;

    [SerializeField]
    private float animSpeedRate;

    public static Color DEFAULT_COLOR = Color.white;
    public static Color ANIM_COLOR = new Color(.6f, .8f, .8f);
    public static Color HAS_INTERNET_COLOR = new Color(0f, 1f, .4f);
    public static Color HAS_NO_INTERNET_COLOR = new Color(1f, 0f, 0f);

    public bool IsDone => isFinished;

    private void Awake()
    {
        iconFragments = new Image[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            iconFragments[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    private void Start () {
        isRunning = false;
        isFinished = true;
	}
	
    public void StartAnim()
    {
        if (!isRunning)
        {
            isRunning = true;
            isFinished = false;
            StartCoroutine(AnimIcon());
        }
    }

    public void StopAnim()
    {
        isRunning = false;
    }

    public void ShowIcon(bool hasInternet)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (hasInternet)
                iconFragments[i].color = HAS_INTERNET_COLOR;
            else
                iconFragments[i].color = HAS_NO_INTERNET_COLOR;
        }
    }

    private void ResetAnim()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            iconFragments[i].color = DEFAULT_COLOR;
        }
    }

    private IEnumerator AnimIcon()
    {
        while(isRunning)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                iconFragments[i].color = ANIM_COLOR;
                int previousIndex = i > 0 ? i - 1 : transform.childCount - 1;
                iconFragments[previousIndex].color = DEFAULT_COLOR;
                yield return new WaitForSeconds(animSpeedRate);
            }
        }
        ResetAnim();
        isFinished = true;
        yield return null;
    }
}
