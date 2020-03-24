using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject options;
    public GameObject options_hint;
    public GameObject options_hint_2;

    public bool optionsLocked;
    public static Options instance;

    public bool paused = false;

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

        GameObject options = GameObject.Find("Options");
        GameObject options_hint = GameObject.Find("Options - hint");
        GameObject options_hint_2 = GameObject.Find("Options - hint 2");
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && options.activeSelf && !optionsLocked)
        {
            options.SetActive(false);
            options_hint.SetActive(true);
            options_hint_2.SetActive(false);
            Time.timeScale = 1;
        }

        else if (Input.GetMouseButtonDown(1) && !options.activeSelf && !optionsLocked)
        {
            options.SetActive(true);
            options_hint.SetActive(false);
            options_hint_2.SetActive(true);
            paused = togglePause();
        }

        if(Input.GetMouseButtonDown(1) && optionsLocked)
        {
            optionsLocked = false;
            Time.timeScale = 1;
        }

    }
}
