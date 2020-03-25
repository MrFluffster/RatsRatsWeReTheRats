using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;
    public bool fade;
    public bool unfade;
    public float fadeSpeed;

    public Image fadeScreen;

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
        DontDestroyOnLoad(gameObject);
        unfade = true;
    }

    void Update()
    {
        if (fade)
        {
            Debug.Log("Fade");
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fade = false;
            }
        }
        if (unfade)
        {
            Debug.Log("Unfade");
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                unfade = false;
            }
        }
    }

    public void FadeToBlack()
    {
        fade = true;
        unfade = false;
    }

    public void FadeFromBlack()
    {
        unfade = true;
        fade = false;
    }
}
