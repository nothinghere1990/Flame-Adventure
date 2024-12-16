using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitBeforeRespawning;
    public bool respawning;

    private PlayerController player;
    public Vector3 respawnPoint;
    
    public Action camSnap;

    public int currentCoins, coinThreshold, currentCrystals;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        player = FindObjectOfType<PlayerController>();
        respawnPoint = player.transform.position;
        coinThreshold = 5;

        currentCoins = PlayerPrefs.GetInt("Coins");
        currentCrystals = PlayerPrefs.GetInt("Crystals");
    }

    private void Start()
    {
        UIController.instance.FadeFromBlack();

        UIController.instance.coinText.text = currentCoins.ToString();
        UIController.instance.crystalText.text = currentCrystals.ToString();
    }

    public void Respawn()
    {
        if (!respawning)
        {
            respawning = true;
            StartCoroutine(RespawnCo());
        }
    }

    IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        UIController.instance.FadeToBlack();
        
        yield return new WaitForSeconds(waitBeforeRespawning);
        
        player.transform.position = respawnPoint;
        
        camSnap?.Invoke();
        
        player.gameObject.SetActive(true);
        
        respawning = false;
        
        UIController.instance.FadeFromBlack();
        
        PlayerHealthController.instance.FillHealth();
    }

    public void GetCoin()
    {
        currentCoins++;

        if (currentCoins >= coinThreshold)
        {
            GetCrystal();

            currentCoins -= coinThreshold;
        }

        UIController.instance.coinText.text = currentCoins.ToString();
        
        PlayerPrefs.SetInt("Coins", currentCoins);
    }
    
    public void GetCrystal()
    {
        currentCrystals++;

        UIController.instance.crystalText.text = currentCrystals.ToString();
        PlayerPrefs.SetInt("Crystals", currentCrystals);
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            print("Reset amounts");
        }
        #endif
    }
}
