using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public Text nameText;
    public Image portrait;
    public string[] dialog;
    public bool playerInRange;
    public int currentLine;

    public bool beginQuest, triggerQuest;
    public int questID;
    public string questTrigger;
    public bool cameraLocked;

    public bool instantlyActivated;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameManager.instance.dialogBox;
        dialogText = GameManager.instance.dialogText;
        nameText = GameManager.instance.nameText;
        portrait = GameManager.instance.portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogBox == null)
        {
            dialogBox = GameManager.instance.dialogBox;
            dialogText = GameManager.instance.dialogText;
            nameText = GameManager.instance.nameText;
            portrait = GameManager.instance.portrait;
        }
        if ((Input.GetButtonDown("Fire1") || (instantlyActivated && currentLine == 0)) && playerInRange)
        {
            if (dialogBox.activeInHierarchy && (currentLine >= dialog.Length))
            {
                ExitDialogue();
            }
            else
            {            
                dialogBox.SetActive(true);
                //CheckIfName();
                CheckIfCommand();
                if (cameraLocked)
                {
                    CameraContorller.instance.lerpSpeed = 0.03f;
                    CameraContorller.instance.anchor = transform;
                    CameraContorller.instance.cameraSize = 4f;
                    Debug.Log(CameraContorller.instance.anchor.name);
                    PlayerController.instance.playerLocked = true;
                }
                if (currentLine < dialog.Length)
                {
                    dialogText.text = dialog[currentLine];
                    currentLine++;
                }             
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
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        CameraContorller.instance.anchor = PlayerController.instance.transform;
        CameraContorller.instance.cameraSize = 5f;
        CameraContorller.instance.lerpSpeed = 0.2f;
        PlayerController.instance.playerLocked = false;
        currentLine = 0;

        playerInRange = false;

        if (beginQuest)
        {
            QuestManager.instance.questList[questID].questStarted = true;
        }
        if(triggerQuest)
        {
            QuestManager.instance.TriggerQuest(questID, questTrigger);
        }

        dialogBox.SetActive(false);
    }

    private void CheckIfName()
    {
        while(dialog[currentLine].StartsWith("--"))
        {
            nameText.text = dialog[currentLine].Replace("--", "") + ":";
            portrait.sprite = GameManager.instance.GetPortrait(dialog[currentLine].Replace("--", ""));
            Debug.Log(dialog[currentLine].Replace("--", ""));
            currentLine++;
        }
    }

    private void CheckIfCommand()
    {
        while(currentLine < dialog.Length && dialog[currentLine].StartsWith("--"))
        {
            if(dialog[currentLine] == "--camera")
            {
                cameraLocked = !cameraLocked;
                Debug.Log("Psuje!");
            }
            else if (dialog[currentLine].StartsWith("--quest_"))
            {
                int.TryParse(dialog[currentLine].Replace("--quest_", ""), out questID);
            }
            else if (dialog[currentLine].StartsWith("--trigger_"))
            {
                QuestManager.instance.SwitchTrigger(questID, dialog[currentLine].Replace("--trigger_", ""));
            }
            else
            {
                nameText.text = dialog[currentLine].Replace("--", "") + ":";
                portrait.sprite = GameManager.instance.GetPortrait(dialog[currentLine].Replace("--", ""));
            }
            Debug.Log(dialog[currentLine]);
            currentLine++;
        }
    }
}
