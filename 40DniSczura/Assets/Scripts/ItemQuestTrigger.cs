using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestTrigger : MonoBehaviour
{
    public int questID;
    public string triggerName;
    public int itemID;
    public int requiredAmount;
    private int counter;
    private bool removeItems = true;
    private bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter = 0;
        for(int i = 0; i < PlayerController.instance.inventory.Length; i++)
        {
            if(PlayerController.instance.inventory[i] == itemID)
            {
                counter++;
            }
        }
        if(counter >= requiredAmount && playerInRange)
        {
            if (QuestManager.instance.questList[questID].questStarted && !QuestManager.instance.questList[questID].questFinished)
            {
                QuestManager.instance.SwitchTrigger(questID, triggerName);
            }
            if(removeItems)
            {
                PlayerController.instance.RemoveItems(itemID);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
