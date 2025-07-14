using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBoom : MonoBehaviour
{
    private IEnumerator CountdownDeath()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(CountdownDeath());  
    }
   
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "House")
        {
            collision = true;  
        }
    }

    bool collision = false;

    void Update() 
    {
        if (collision == true) 
        {
            GameObject other = GameObject.Find("Player");
            other.GetComponent<PlayerFinishDeath>().OnDeath();
            collision = false;
        }
    }
}
