using Unity.Mathematics;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   public GameObject effect;
   public Transform effectPoint;

   public Animator anim;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         if (LevelManager.instance.respawnPoint != transform.position)
         {
            LevelManager.instance.respawnPoint = transform.position;

            if (effect != null)
               Instantiate(effect, effectPoint.position, quaternion.identity);

            //Deactive all checkpoint.
            Checkpoint[] allCP = FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint cp in allCP)
            {
               cp.anim.SetBool("active", false);
            }
            
            anim.SetBool("active", true);
         }
      }
   }
}
