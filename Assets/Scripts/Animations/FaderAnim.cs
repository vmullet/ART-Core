using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FaderAnim : MonoBehaviour,IAnim {

    private Image fadeImg;
    private bool isRunning;

    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private bool fadeInMode;

    const float DEFAULT_FADE_DURATION = 1f;

    public bool IsDone => !isRunning;
    public bool FadeInMode
    {
        get { return fadeInMode; }
        set { fadeInMode = value; }
    }

    private void Awake()
    {
        fadeImg = GetComponent<Image>();
    }

    private void Start () {
        if (fadeDuration < 0)
            fadeDuration = DEFAULT_FADE_DURATION;
        fadeImg.color = new Color(0, 0, 0, 1f);
        fadeInMode = true;
        StartAnim();
    }

    private IEnumerator Anim()
    {
        yield return new WaitForSeconds(.5f);
        if (fadeInMode)
            fadeImg.CrossFadeAlpha(0f, fadeDuration, true);
        else
            fadeImg.CrossFadeAlpha(1f, fadeDuration, true);

        yield return new WaitForSeconds(fadeDuration);
        isRunning = false;
        yield return null;

    }
    
    public void StartAnim()
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(Anim());
        }
    }

    public void StopAnim()
    {
        StopAllCoroutines();
        isRunning = false;
    }

}
