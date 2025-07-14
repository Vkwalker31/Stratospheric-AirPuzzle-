using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPath : MonoBehaviour
{
    public float speed = 1f; 
    Vector3 pos1; 
    Vector3 pos2; 
    public float offset;

    float lastpos;
    public SpriteRenderer sprite;
    
    public GameObject target;

    void Start() 
    {
        pos1 = transform.position;
        pos2 = target.transform.position;
    }
 
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed + offset, 1f));

        if (transform.position.x < lastpos)
        {
            sprite.flipX = false;
            lastpos = transform.position.x;
        }

        else if (transform.position.x > lastpos)
        {
            sprite.flipX = true;
            lastpos = transform.position.x;
        }
    }

}
