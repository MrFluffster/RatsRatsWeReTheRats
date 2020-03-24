using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Sprite[] characterPortraits;
    public string[] characterNames;
    public Sprite[] itemIcons;

    public GameObject inventoryBar;
    public Image[] inventorySlots;

    public Text dialogText;
    public Text nameText;
    public Image portrait;
    public GameObject dialogBox;
    public Slider deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Making sure ONLY one GM is alive
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    //Returns the ID of the given character
    private int FindCharacterID(string name)
    {
        for (int i = 0; i < characterNames.Length; i++)
        {
            if (characterNames[i] == name)
            {
                Debug.Log(i);
                return i;
            }
        }
        return -1;
    }

    //Returns the sprite of a given character
    public Sprite GetPortrait(string name)
    {
        int charID = FindCharacterID(name);

        if (charID != -1)
        {
            return characterPortraits[charID];
        }
        else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.HasItems())
        {
            inventoryBar.SetActive(true);
            for(int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].sprite = itemIcons[PlayerController.instance.inventory[i]];
            }
        }
        else
        {
            inventoryBar.SetActive(false);
        }
    }
}
