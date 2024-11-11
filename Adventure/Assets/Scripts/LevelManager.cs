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
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        respawnPoint = player.transform.position;
        UIController.instance.FadeFromBlack();
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
}
