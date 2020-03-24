using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKiller : MonoBehaviour
{
    private bool playerInRange;
    private bool playerKilled;

    public float deathTimerBase;
    public float deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            PlayerController.instance.gameObject.SetActive(false);
            deathTimer = deathTimerBase;
            playerInRange = false;
            playerKilled = true;
        }

        if(deathTimer >= 0)
        {
            deathTimer -= Time.deltaTime;
            Debug.Log(deathTimer);
        }
        

        if (deathTimer < 0 && playerKilled)
        {
            Debug.Log("respawn");
            for(int i = 0; i < QuestManager.instance.questList[ResetState.instance.questID].questTriggers.Length; i++)
            {
                QuestManager.instance.questList[ResetState.instance.questID].questTriggerStates[i] = ResetState.instance.triggerState[i];
            }
            playerKilled = false;
            PlayerController.instance.gameObject.SetActive(true);
            PlayerController.instance.transitioning = true;
            SceneManager.LoadScene(ResetState.instance.currentScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
}
