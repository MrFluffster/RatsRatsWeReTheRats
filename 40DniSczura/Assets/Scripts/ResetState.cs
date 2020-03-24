using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetState : MonoBehaviour
{
    public static ResetState instance;
    public int questID;
    public bool[] triggerState;
    public string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
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
        
    }
}
