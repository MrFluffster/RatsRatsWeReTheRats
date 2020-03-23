using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject options;
    public GameObject options_hint;
    public GameObject options_hint_2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject options = GameObject.Find("Options");
        GameObject options_hint = GameObject.Find("Options - hint");
        GameObject options_hint_2 = GameObject.Find("Options - hint 2");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && options.activeSelf)
        {
            options.SetActive(false);
            options_hint.SetActive(true);
            options_hint_2.SetActive(false);
        }

        else if (Input.GetMouseButtonDown(1) && !options.activeSelf)
        {
            options.SetActive(true);
            options_hint.SetActive(false);
            options_hint_2.SetActive(true);
        }

    }
}
