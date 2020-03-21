using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public int questID;
    //Is it quest-start activated?
    public bool needsQuestStarted;
    
    public bool needsQuestFinished;

    //Does it need a specific quest trigger?
    public bool activatesOnTrigger;
    public string neededTrigger;

    //This is true if the Activator turns an object on when triggered, false if it deactivates it. 
    public bool state;

    public GameObject objectToActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If either the needed quest is started or the condition is irrelevant
        if((needsQuestStarted && QuestManager.instance.questList[questID].questStarted) || !needsQuestStarted)
        {
            //If either the quest is finished or the condition irrelevant
            if((needsQuestFinished && QuestManager.instance.questList[questID].questFinished) || !needsQuestFinished)
            {
                if((activatesOnTrigger && QuestManager.instance.GetTriggerState(questID, neededTrigger)) || !activatesOnTrigger)
                {
                    objectToActivate.SetActive(state);
                }
            }
        }
    }
}
