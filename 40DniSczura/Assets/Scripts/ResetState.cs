using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetState : MonoBehaviour
{
    public static ResetState instance;
    public int questID;
    public bool[] triggerState;
    public string currentScene;
    public Slider sliderOfDeath;

    // Start is called before the first frame update
    void Start()
    {
        sliderOfDeath.gameObject.SetActive(false);
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
