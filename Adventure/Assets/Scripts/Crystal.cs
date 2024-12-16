using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{
    public GameObject pickupEffect;

    public string uniqueID;

    private void Start()
    {
        if (PlayerPrefs.HasKey(uniqueID) && PlayerPrefs.GetInt(uniqueID) == 1)
            gameObject.SetActive(false);
        
        uniqueID = SceneManager.GetActiveScene().name + uniqueID;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.GetCrystal();

            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, quaternion.identity);
            
            PlayerPrefs.SetInt(uniqueID, 1);
            
            Destroy(gameObject);
        }
    }
}
