using Unity.Mathematics;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject pickupEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.GetCoin();

            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
