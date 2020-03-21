using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Sprite[] characterPortraits;
    public string[] characterNames;

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
        
    }
}
