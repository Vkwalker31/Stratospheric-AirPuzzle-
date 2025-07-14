﻿using UnityEngine;

public class GoalLine : MonoBehaviour
{
    GameObject pos1;
    GameObject pos2;
    LineRenderer lr;
    
    void Start() 
    {
        pos1 = GameObject.Find("GoalTarget");
        pos2 = GameObject.Find("PlayerTarget");
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, pos1.transform.position);
        lr.SetPosition(1, pos2.transform.position); 
    }
}
