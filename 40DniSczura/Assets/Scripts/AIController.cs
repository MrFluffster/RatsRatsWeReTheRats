using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform[] patrolRoute;
    public int currentGoal;
    public bool patrolling;
    public bool canChase;
    public bool playerInRange;
    private Rigidbody2D rigidBody;

    public float moveSpeed;
    public bool atPatrolPoint;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Do patrol route
        if(patrolling && !playerInRange)
        {
            //Debug.Log("EEEEEEEEEEEEE");
            if (atPatrolPoint)
            {
                rigidBody.velocity = (patrolRoute[currentGoal].position - transform.position).normalized * moveSpeed;
            }
        }

        //Chase the player
        if(canChase && playerInRange)
        {
            audioSource.Play();
            //Debug.Log("AAAAAAAA");
            rigidBody.velocity = (PlayerController.instance.transform.position - transform.position).normalized * moveSpeed;
        }

        //Return to origin point
        if(!playerInRange && !patrolling)
        {
            
            if (atPatrolPoint)
            {
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                rigidBody.velocity = (patrolRoute[0].position - transform.position).normalized * moveSpeed;
            }
        }

        rigidBody.MoveRotation(Mathf.Rad2Deg * Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) - 135f);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(patrolling && !playerInRange)
        {
            rigidBody.velocity = (patrolRoute[currentGoal].position - transform.position).normalized * moveSpeed;
        }
        if (!patrolling && !playerInRange)
        {
            rigidBody.velocity = (patrolRoute[0].position - transform.position).normalized * moveSpeed;
        }
    }
}
