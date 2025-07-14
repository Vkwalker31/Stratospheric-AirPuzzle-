using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject levelMenu;

    private string levelName;

    void Start()
    {
        Invoke("DisableOverlay", 1f);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
    }

    void DisableOverlay() 
    {
        overlay.SetActive(false);
    }

    public void StartButton() 
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        levelMenu.SetActive(true);
    }

    public void ExitStart() 
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        levelMenu.SetActive(false);
    }

    public void OptionsButton() 
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        optionsMenu.SetActive(true);
    }

    public void ExitOptions() 
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        optionsMenu.SetActive(false);
    }

    public void OpenLevel(int levelID)
    {
        FindFirstObjectByType<AudioManager>().Play("Click");
        if (levelID == 6)
            levelName = "DayToNight";
        else
            levelName = "Level " + levelID;
        StartCoroutine(StartLevel());
    }

    public IEnumerator StartLevel()
    {
        overlay.SetActive(true);
        animator.SetBool("FadeIn",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }

}
