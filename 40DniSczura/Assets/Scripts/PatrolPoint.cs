using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        AIController patrolling = collision.GetComponent<AIController>();
        if(patrolling != null)
        {
            patrolling.atPatrolPoint = true;
            if (patrolling.currentGoal >= patrolling.patrolRoute.Length - 1)
            {
                patrolling.currentGoal = 0;
            }
            else
            {
                patrolling.currentGoal++;
            }
            patrolling.transform.position = transform.position;
            Debug.Log(patrolling.currentGoal);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        AIController patrolling = collision.GetComponent<AIController>();
        if (patrolling != null)
        {
            patrolling.atPatrolPoint = false;
        }
    }
}
