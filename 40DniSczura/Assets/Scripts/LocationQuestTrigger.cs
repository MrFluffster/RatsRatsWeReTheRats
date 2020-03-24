using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationQuestTrigger : MonoBehaviour
{
    public int questID;
    public string triggerName;

    public string neededTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == neededTag)
        {
            if(QuestManager.instance.questList[questID].questStarted && !QuestManager.instance.questList[questID].questFinished)
            {
                QuestManager.instance.SwitchTrigger(questID, triggerName);
            }
        }
    }
}
