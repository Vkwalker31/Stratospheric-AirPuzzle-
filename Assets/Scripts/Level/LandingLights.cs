using UnityEngine;

public class LandingLights : MonoBehaviour
{
    GameObject pos1;
    GameObject pos2;
    
    public float distanceToGoal;
    public float distanceToSpawn;

    public Animator animator;

    void Start()
    {
        pos1 = GameObject.Find("Player");
        pos2 = GameObject.Find("Goal");
    }
    void Update()
    {
        distanceToGoal = Vector3.Distance (pos1.transform.position, pos2.transform.position); 
        distanceToSpawn = Vector3.Distance (pos1.transform.position, Vector3.zero); 
        animator.SetFloat("Distance", Mathf.Min(distanceToGoal, distanceToSpawn));
    }
}
