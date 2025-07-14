using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    [Header("Text")]
    public Text introText;
    public float typingSpeed = 0.05f;
    public AudioSource typeSound;

    [Header("Image")]
    public Image storyImage;
    public float imageFadeInTime = 2.0f;

    [Header("Transition settings")]
    public string nextSceneName = "TitleScreen";
    public float autoTransitionDelay = 3.0f;

    [TextArea(3, 10)]
    public string[] storyLines = new string[]
    {
        "НЕСМОТРЯ НА МНОГИЕ ОПАСЕНИЯ,",
        "КОТОРЫЕ ПРИВЕЛИ К ТОМУ, ЧТО",
        "МНОГИЕ СТРАНЫ ЗАПРЕТИЛИ ЕГО,",
        "ОДИН СМЕЛЬЧАК СМОГ ЭТО",
        "ОПРОВЕРГНУТЬ..."
    };

    private bool textComplete = false;
    private bool imageComplete = false;

    void Start()
    {
        if (introText != null)
        {
            introText.text = "";
        }

        if (storyImage != null)
        {
            Color c = storyImage.color;
            c.a = 0f;
            storyImage.color = c;
        }
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        yield return StartCoroutine(FadeInImage());
        yield return StartCoroutine(TypeText());
        yield return new WaitForSeconds(autoTransitionDelay);
        LoadNextScene();
    }

    IEnumerator FadeInImage()
    {
        if (storyImage == null)
        {
            imageComplete = true;
            yield break;
        }

        float elapsed = 0f;
        Color color = storyImage.color;

        while (elapsed < imageFadeInTime)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / imageFadeInTime);
            storyImage.color = color;
            yield return null;
        }
        color.a = 1f;
        storyImage.color = color;
        imageComplete = true;
    }

    IEnumerator TypeText()
    {
        if (introText == null)
        {
            textComplete = true;
            yield break;
        }

        string fullText = string.Join("\n", storyLines);

        if (typeSound != null)
        {
            typeSound.loop = true;
            typeSound.pitch = 1f;
            if (!typeSound.isPlaying)
                typeSound.Play();
        }

        foreach (char letter in fullText)
        {
            introText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (typeSound != null && typeSound.isPlaying)
            typeSound.Stop();

        textComplete = true;
    }

    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!imageComplete || !textComplete)
            {
                StopAllCoroutines();
                ShowEverything();
            }
            else
            {
                LoadNextScene();
            }
        }
    }

    void ShowEverything()
    {
        if (storyImage != null)
        {
            Color c = storyImage.color;
            c.a = 1f;
            storyImage.color = c;
        }

        if (introText != null)
        {
            introText.text = string.Join("\n", storyLines);
        }

        if (typeSound != null && typeSound.isPlaying)
            typeSound.Stop();

        imageComplete = true;
        textComplete = true;
        StartCoroutine(DelayedTransition(1.5f));
    }

    IEnumerator DelayedTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (SceneManager.GetSceneByName(nextSceneName).IsValid() || Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Scene '" + nextSceneName + "' does not exist in build settings!");
        }
    }
}