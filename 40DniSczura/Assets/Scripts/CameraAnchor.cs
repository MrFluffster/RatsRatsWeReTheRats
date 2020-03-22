using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    public bool triggerActivated;
    public bool collisionActivated;

    public bool playerInRange;
    public string reqiredTrigger;
    public bool requiredTriggerState;
    public int questID;
    public float cameraSize;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerActivated && !collisionActivated)
        {
            if(QuestManager.instance.questList[questID].questStarted && QuestManager.instance.GetTriggerState(questID, reqiredTrigger) == requiredTriggerState)
            {
                CameraContorller.instance.anchor = transform;
                CameraContorller.instance.cameraSize = cameraSize;
                CameraContorller.instance.lerpSpeed = lerpSpeed;
            }
        }
        else if (collisionActivated && !triggerActivated)
        {
            if (playerInRange)
            {
                CameraContorller.instance.anchor = transform;
                CameraContorller.instance.cameraSize = cameraSize;
                CameraContorller.instance.lerpSpeed = lerpSpeed;
            }
        }
        else
        {
            if (playerInRange && QuestManager.instance.questList[questID].questStarted && QuestManager.instance.GetTriggerState(questID, reqiredTrigger) == requiredTriggerState)
            {
                CameraContorller.instance.anchor = transform;
                CameraContorller.instance.cameraSize = cameraSize;
                CameraContorller.instance.lerpSpeed = lerpSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            CameraContorller.instance.anchor = PlayerController.instance.transform;
            CameraContorller.instance.cameraSize = 5;
            CameraContorller.instance.lerpSpeed = 0.2f;
        }
    }
}
