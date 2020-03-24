using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string sceneToLoad;
    public string transitionID;

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
        if(collision.CompareTag("Player"))
        {
            PlayerController.instance.transitioning = true;
            PlayerController.instance.transitionID = transitionID;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
