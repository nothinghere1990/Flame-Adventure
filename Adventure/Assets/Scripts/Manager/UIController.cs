using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image fadeScreen;
    public bool isFadingToBlack, isFadingFromBlack;
    public float fadeSpeed;

    public Slider healthSlider;
    public TMP_Text healthText, coinText, crystalText;

    public GameObject pauseScreen;

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

        if (Input.GetKeyDown(KeyCode.Escape)) PauseUnpause();
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

    public void UpdateHealthDisplay(int health)
    {
        healthText.text = "Health: " + health + "/" + PlayerHealthController.instance.maxHealth;

        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = health;
    }

    public void PauseUnpause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);

        if (pauseScreen.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1f;
        }
    }
}