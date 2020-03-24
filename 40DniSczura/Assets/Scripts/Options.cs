using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject options;
    public GameObject options_hint;
    public GameObject options_hint_2;

    public AudioSource audioSource;

    public bool optionsLocked;
    public static Options instance;

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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && options.activeSelf && !optionsLocked)
        {
            audioSource.Play();
            options.SetActive(false);
            options_hint.SetActive(true);
            options_hint_2.SetActive(false);
            Time.timeScale = 1f;
        }

        else if (Input.GetMouseButtonDown(1) && !options.activeSelf && !optionsLocked)
        {
            audioSource.Play();
            options.SetActive(true);
            options_hint.SetActive(false);
            options_hint_2.SetActive(true);
            Time.timeScale = 0f;
        }

        if(Input.GetMouseButtonDown(1) && optionsLocked)
        {
            optionsLocked = false;
        }

    }
}
