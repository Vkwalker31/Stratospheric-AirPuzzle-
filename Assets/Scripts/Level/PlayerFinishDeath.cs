using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFinishDeath : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Finish") 
        {
            OnFinish();
        }

        if (other.tag == "DeathZone") 
        {
            OnDeath();
        }
    }

    public Animator animator;

    public PlayerController script1;
    public FuelBar script2;
    public GameObject confetti;

    public AudioSource boostsound; 

    void OnFinish() 
    {
        if (script1.input == true) 
        {
            script1.enabled = false;
            script2.enabled = false;
            overlay.SetActive(true);
        
            boostsound.mute = true; 
            hasrun = true; 

            confetti.SetActive(true);
            animator.SetBool("Finish",true);

            FindFirstObjectByType<AudioManager>().Play("Finish");

            Invoke(nameof(NextScene), 2f);
        }
    }

    void NextScene() 
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1) 
        {
            SceneManager.LoadScene("TitleScreen");
        } 
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    bool hasrun = false; 
    public GameObject overlay;
    
    public void OnDeath() 
    {
        if (hasrun == false) 
        {
            overlay.SetActive(true);
            animator.SetBool("OnDeath",true);
            script2.enabled = false;
            script1.input = false; 
        
            Invoke ("ReloadScene", 2f);  

            FindFirstObjectByType<AudioManager>().Play("Fail");
            hasrun = true;
        }
    }

    void ReloadScene() 
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }   
}
