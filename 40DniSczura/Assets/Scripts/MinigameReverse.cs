using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameReverse : MonoBehaviour
{
    private bool playerInRange;

    //Must be LESS than sequence timer!
    public float displayTimerBase;
    public float displayTimer;
    public float displayTimerReverseBase;
    public float displayTimerReverse;

    public float reverseTimerBase;
    public List<float> reverseTimers;
    public List<int> reversedOutputs;
    public List<bool> outputHit;

    public bool playerActivated;

    public int pointMeter;
    public int lastInput;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Start minigame
        if (Input.GetButtonDown("Fire1") && !playerActivated && playerInRange && !minigameCompleted)
        {
            playerActivated = true;
            minigameUI.SetActive(true);
            PlayerController.instance.playerLocked = true;
            Options.instance.optionsLocked = true;
        }

        //Tick down the timers
        if (displayTimer >= 0)
        {
            displayTimer -= Time.deltaTime;
        }
        if (displayTimerReverse >= 0)
        {
            displayTimerReverse -= Time.deltaTime;
        }
        for (int i = 0; i < reverseTimers.Count; i++)
        {
            if (reverseTimers[i] >= 0)
            {
                reverseTimers[i] -= Time.deltaTime;
            }
        }

        //Input feedback stuff
        if (displayTimer < 0)
        {
            arrows[lastInput].sprite = unactiveArrow;
        }
        if(reverseTimers.Count != 0 && reverseTimers[0] < 0 && reverseTimers[0] != -1)
        {
            displayTimerReverse = displayTimerReverseBase;
            reverseTimers[0] = -1;
            arrows[reversedOutputs[0]].sprite = activeArrow;
        }
        if (displayTimerReverse < 0 && reversedOutputs.Count != 0 && reverseTimers[0] < 0)
        {
            arrows[reversedOutputs[0]].sprite = unactiveArrow;
            if(!outputHit[0])
            {
                pointMeter = 0;
            }
            reversedOutputs.RemoveRange(0, 1);
            reverseTimers.RemoveRange(0, 1);
            outputHit.RemoveRange(0, 1);
        }

        //The reverse display timers are the window for player input and after them the list objects are removed

        //Minigame functionality
        if(playerActivated)
        {
            int playerInput = -1;
            if (Input.GetKeyDown(KeyCode.W)) playerInput = 0;
            else if (Input.GetKeyDown(KeyCode.D)) playerInput = 1;
            else if (Input.GetKeyDown(KeyCode.S)) playerInput = 2;
            else if (Input.GetKeyDown(KeyCode.A)) playerInput = 3;

            if (playerInput != -1 && displayTimer < 0)
            {
                arrows[playerInput].sprite = activeArrow;
                displayTimer = displayTimerBase;
                lastInput = playerInput;
                //Create a reversed output in the lists
                int reverse;
                if (playerInput > 1) reverse = playerInput - 2;
                else reverse = playerInput + 2;
                reversedOutputs.Add(reverse);
                reverseTimers.Add(reverseTimerBase);
                outputHit.Add(false);

                //Debug.Log("Player: " + playerInput);
                if ((displayTimerReverse >= 0 || reverseTimers[0] < 0.1f) && playerInput == reversedOutputs[0])
                {
                    pointMeter++;
                    outputHit[0] = true;
                }
                else
                {
                    //TODO Give extra feedback
                    pointMeter = 0;
                }
            }

            //End minigame
            if (pointMeter >= 3)
            {
                EndMinigame(true);
            }
        }
        

        //Abort
        if (Input.GetButtonDown("Fire2") && playerActivated && playerInRange)
        {
            Debug.Log("Koniec");
            EndMinigame(false);
        }

    }

    public void EndMinigame(bool success)
    {
        if (success)
        {
            minigameCompleted = true;
            //Issue rewards
            if (rewardWithItem)
            {
                PlayerController.instance.AddItem(itemID);
            }
            if (rewardWithQuestTrigger && QuestManager.instance.questList[questID].questStarted)
            {
                QuestManager.instance.TriggerQuest(questID, trigger, triggerState);
            }
        }

        //Reset minigame
        displayTimer = 0;
        displayTimerReverse = 0;
        reversedOutputs.Clear();
        reverseTimers.Clear();
        playerActivated = false;
        minigameUI.SetActive(false);
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
