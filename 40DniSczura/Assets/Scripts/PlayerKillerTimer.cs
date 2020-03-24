using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerKillerTimer : MonoBehaviour
{
    public Slider timerDisplay;
    public float timerBase;
    public float timer;

    private bool playerKilled;

    public float deathTimerBase;
    public float deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        timerDisplay.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
            deathTimer = deathTimerBase;
            playerKilled = true;
        }

        if (deathTimer >= 0)
        {
            deathTimer -= Time.deltaTime;
            Debug.Log(deathTimer);
        }
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            float x = (timer / timerBase);
            timerDisplay.value = x;
        }


        if (deathTimer < 0 && playerKilled)
        {
            Debug.Log("respawn");
            for (int i = 0; i < QuestManager.instance.questList[ResetState.instance.questID].questTriggers.Length; i++)
            {
                QuestManager.instance.questList[ResetState.instance.questID].questTriggerStates[i] = ResetState.instance.triggerState[i];
            }
            playerKilled = false;
            PlayerController.instance.gameObject.SetActive(true);
            PlayerController.instance.transitioning = true;
            SceneManager.LoadScene(ResetState.instance.currentScene);
        }
    }
}
