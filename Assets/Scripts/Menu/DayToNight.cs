using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayToNight : MonoBehaviour
{
    [Header("UI References")]
    public Image storyImage;
    public Text storyText;
    public AudioSource typeSound;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;

    [Header("Animation Settings")]
    public float fadeTime = 1.5f;
    public float delayBetweenScenes = 2f;

    [Header("Images and Texts")]
    public Sprite[] storySprites;
    [TextArea(3, 10)]
    public string[] storyTexts;

    [Header("Next Level")]
    public string nextLevelName = "Level 6";

    private int currentStage;
    private bool isFastForward = false;
    private Coroutine typingCoroutine = null;

    void Start()
    {
        currentStage = 0;
        isFastForward = false;
        SetImageAlpha(0f);
        storyText.text = "";
        storyImage.sprite = storySprites[0];
        StartCoroutine(ShowStoryStage(0));
    }

    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            SkipOrContinue();
        }
    }

    void SkipOrContinue()
    {
        isFastForward = true;
    }

    IEnumerator ShowStoryStage(int stage)
    {
        isFastForward = false;
        currentStage = stage;
        yield return null;

        if (storyImage.sprite != storySprites[stage])
            storyImage.sprite = storySprites[stage];

        yield return StartCoroutine(FadeImageIn());
        typingCoroutine = StartCoroutine(TypeTextEffect(storyTexts[stage]));

        while (typingCoroutine != null)
        {
            if (isFastForward)
            {
                StopCoroutine(typingCoroutine);
                storyText.text = storyTexts[stage];
                typingCoroutine = null;
                break;
            }
            yield return null;
        }

        isFastForward = false;
        float t = 0;
        while (t < delayBetweenScenes)
        {
            if (isFastForward) break;
            t += Time.deltaTime;
            yield return null;
        }

        if (stage == 0)
        {
            yield return StartCoroutine(FadeImageOut());
            currentStage = 1;
            SetImageAlpha(0f);
            storyText.text = "";
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(ShowStoryStage(1));
        }
        else
        {
            yield return StartCoroutine(FadeImageOut());
            yield return StartCoroutine(FadeScreenOut());
            SceneManager.LoadScene(nextLevelName);
        }
    }

    IEnumerator FadeImageIn()
    {
        float t = 0;
        while (t < fadeTime)
        {
            if (isFastForward) break;
            SetImageAlpha(t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        SetImageAlpha(1f);
    }

    IEnumerator FadeImageOut()
    {
        float t = fadeTime;
        while (t > 0)
        {
            SetImageAlpha(t / fadeTime);
            t -= Time.deltaTime;
            yield return null;
        }
        SetImageAlpha(0f);
    }

    void SetImageAlpha(float a)
    {
        if (storyImage != null)
        {
            Color c = storyImage.color;
            c.a = Mathf.Clamp01(a);
            storyImage.color = c;
        }
    }

    IEnumerator TypeTextEffect(string message)
    {
        storyText.text = "";
        if (typeSound != null)
        {
            typeSound.loop = true;
            if (!typeSound.isPlaying)
                typeSound.Play();
        }

        foreach (char letter in message)
        {
            if (isFastForward)
            {
                break;
            }
            storyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (typeSound != null && typeSound.isPlaying)
            typeSound.Stop();

        storyText.text = message;
        typingCoroutine = null;
    }

    public Image fadePanel;
    public float screenFadeTime = 1f;

    IEnumerator FadeScreenOut()
    {
        if (fadePanel == null) yield break;
        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.gameObject.SetActive(true);
        float t = 0;
        while (t < screenFadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / screenFadeTime);
            fadePanel.color = c;
            yield return null;
        }
        c.a = 1f;
        fadePanel.color = c;
    }
}