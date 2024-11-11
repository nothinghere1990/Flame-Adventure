using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    
    private int currentHealth;
    public int maxHealth;

    public float invincibilityLength = 1f;
    private float invincCounter;

    public GameObject[] modelDisplay;
    private float flashCounter;
    public float flashTime = .1f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FillHealth();
    }

    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                flashCounter = flashTime;

                foreach (var piece in modelDisplay)
                {
                    piece.SetActive(!piece.activeSelf);
                }
            }

            if (invincCounter <= 0)
            {
                foreach (var piece in modelDisplay)
                {
                    piece.SetActive(true);
                }
            }
        }
    }
    
    public void DamagePlayer()
    {
        if (invincCounter <= 0)
        {
            invincCounter = invincibilityLength;
            
            currentHealth--;
            
            if (currentHealth <= 0) LevelManager.instance.Respawn();
            
            UIController.instance.UpdateHealthDisplay(currentHealth);
        }
    }
    
    public void FillHealth()
    {
        currentHealth = maxHealth;
        
        UIController.instance.UpdateHealthDisplay(currentHealth);
    }
}
