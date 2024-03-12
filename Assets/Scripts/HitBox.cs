using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health Health;
    
    
   public void OnRaycastHit(BulletMove weapon,Vector3 direction)
   {
        Debug.Log("GOGOGO");
        Health.TakeDamage(weapon.damege,direction);
        Debug.Log("GOGOGO");
                      /*
                      var hitBox=hitInfo.collider.GetComponent<HitBox>
                      if(hitBox)
                      {
                       hitBox.OnRaycastHit(this,ray.direction);
                       }
                      */
                      

  }
    
}
