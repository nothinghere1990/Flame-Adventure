using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image fadeScreen;
    public bool isFadingToBlack, isFadingFromBlack;
    public float fadeSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (isFadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1, fadeSpeed * Time.deltaTime));
        }

        if (isFadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0, fadeSpeed * Time.deltaTime));
        }
    }

    public void FadeToBlack()
    {
        isFadingToBlack = true;
        isFadingFromBlack = false;
    }
    
    public void FadeFromBlack()
    {
        isFadingToBlack = false;
        isFadingFromBlack = true;
    }
}