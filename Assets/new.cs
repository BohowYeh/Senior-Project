using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New:MonoBehaviour
{
    // Start is called before the first frame update


    void OnTriggerEnter(Collider other)
    {
        //當碰到玩家時銷毀自己
        if (other.gameObject.CompareTag("Player"))
        {
           
          
            Destroy(gameObject);
        }
    }
}
