using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameQuick : MonoBehaviour
{
    private bool playerInRange;
    public int[] sequence;

    public float startTimerBase;
    public float startTimer;

    public float sequenceTimerBase;
    public float sequenceTimer;

    //Must be LESS than sequence timer!
    public float displayTimerBase;
    public float displayTimer;

    public bool sequenceDisplayed;
    public bool playerActivated;
    public int numberInSequence;

    public GameObject minigameUI;
    public SpriteRenderer[] arrows;
    public Sprite unactiveArrow;
    public Sprite activeArrow;

    public bool minigameCompleted;

    public bool rewardWithItem;
    public bool rewardWithQuestTrigger;

    public int questID, itemID;
    public string trigger;
    public bool triggerState;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !playerActivated && playerInRange && !minigameCompleted)
        {
            playerActivated = true;
            startTimer = startTimerBase;
            minigameUI.SetActive(true);
            PlayerController.instance.playerLocked = true;
            Options.instance.optionsLocked = true;
        }

        //Tick down the timers
        if (startTimer >= 0)
        {
            startTimer -= Time.deltaTime;
        }
        if (sequenceTimer >= 0)
        {
            sequenceTimer -= Time.deltaTime;
        }
        if (displayTimer >= 0)
        {
            displayTimer -= Time.deltaTime;
        }

        //Showing the sequence
        if (playerActivated && startTimer < 0 && sequenceTimer < 0 && !sequenceDisplayed)
        {
            if(numberInSequence < sequence.Length)
            {             
                Debug.Log(numberInSequence);
                audioSource.Play();
                arrows[sequence[numberInSequence]].sprite = activeArrow;
                displayTimer = displayTimerBase;
                sequenceTimer = sequenceTimerBase;
                numberInSequence++;
            }
            else
            {
                sequenceDisplayed = true;
                numberInSequence = 0;
            }
        }

        if(displayTimer < 0)
        {
            if(numberInSequence > 0)
            {
                arrows[sequence[numberInSequence - 1]].sprite = unactiveArrow;
            }
        }

        if(sequenceDisplayed)
        {
            int playerInput = -1;
            if (Input.GetKeyDown(KeyCode.W)) playerInput = 0;
            else if (Input.GetKeyDown(KeyCode.D)) playerInput = 1;
            else if (Input.GetKeyDown(KeyCode.S)) playerInput = 2;
            else if (Input.GetKeyDown(KeyCode.A)) playerInput = 3;
            //Debug.Log("Player: " + playerInput);

            if (playerInput == sequence[numberInSequence] && displayTimer < 0)
            {
                Debug.Log("Player: " + numberInSequence);
                audioSource.Play();
                arrows[sequence[numberInSequence]].sprite = activeArrow;
                displayTimer = displayTimerBase;
                numberInSequence++;
            }
            else if(playerInput != -1 && displayTimer < 0)
            {
                numberInSequence = 0;
            }

            //End minigame
            if(numberInSequence >= sequence.Length)
            {
                EndMinigame(true);
            }
        }

        if (Input.GetButtonDown("Fire2") && playerActivated && playerInRange)
        {
            Debug.Log("Koniec");
            EndMinigame(false);
        }

        //Reset timers

        //Minigame ends and the player is unlocked
    }

    public void EndMinigame(bool success)
    {
        if(success)
        {
            minigameCompleted = true;
            //Issue rewards
            if(rewardWithItem)
            {
                PlayerController.instance.AddItem(itemID);
            }
            if(rewardWithQuestTrigger && QuestManager.instance.questList[questID].questStarted)
            {
                QuestManager.instance.TriggerQuest(questID, trigger, triggerState);
            }
        }

        //Reset minigame
        if (numberInSequence > 0)
        {
            arrows[sequence[numberInSequence - 1]].sprite = unactiveArrow;
        }
        sequenceTimer = -1;
        displayTimer = 0;
        startTimer = 0;
        playerActivated = false;
        sequenceDisplayed = false;
        minigameUI.SetActive(false);
        numberInSequence = 0;
        PlayerController.instance.playerLocked = false;
        //Options.instance.optionsLocked = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
