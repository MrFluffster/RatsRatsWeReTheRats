using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    //The only QuestManager allowed to exist
    public static QuestManager instance;

    //List of all quests with their states
    public Quest[] questList;

    //Something for testing only
    public bool testing;
    public Text testText;

    // Start is called before the first frame update
    void Start()
    {
        //Making sure ONLY one QM is alive
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Quest testing stuff - do triggers work?
        if(testing)
        {
            if (questList[0] != null)
            {

                testText.text = questList[0].questTriggers[0] + ": " + questList[0].questTriggerStates[0];
            }
        }     
    }

    //Returns the ID of the given trigger of a given quest
    private int FindTriggerID(int questID, string triggerName)
    {
        for(int i = 0; i < questList[questID].questTriggers.Length; i++)
        {
            if(questList[questID].questTriggers[i] == triggerName)
            {
                Debug.Log(i);
                return i;
            }
        }
        return -1;
    }

    //Returns the state of the given trigger of a given quest
    public bool GetTriggerState(int questID, string triggerName)
    {
        int triggerID = FindTriggerID(questID, triggerName);

        if (triggerID != -1)
        {
            return questList[questID].questTriggerStates[triggerID];
        }
        else
        {
            return false;
        }
    }

    //Sets the given trigger to a given state
    public void TriggerQuest(int questID, string triggerName, bool triggerState)
    {
        int triggerID = FindTriggerID(questID, triggerName);

        if(triggerID != -1 && questList[questID].questStarted)
        {
            questList[questID].questTriggerStates[triggerID] = triggerState;
        }      
    }

    //Sets the given trigger to 'true'
    public void TriggerQuest(int questID, string triggerName)
    {
        int triggerID = FindTriggerID(questID, triggerName);

        if (triggerID != -1 && questList[questID].questStarted)
        {
            questList[questID].questTriggerStates[triggerID] = true;
        }
    }

    //Marks a quest as started
    public void StartQuest(int questID)
    {
        if(!questList[questID].questStarted)
        {
            questList[questID].questStarted = true;
        }
    }

    //Marks a quest as finished
    public void EndQuest(int questID)
    {
        if(!questList[questID].questFinished && questList[questID].questStarted)
        {
            questList[questID].questFinished = true;
        }
    }
}
