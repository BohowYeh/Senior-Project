using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New:MonoBehaviour
{
    // Start is called before the first frame update


    void OnTriggerEnter(Collider other)
    {
        //��I�쪱�a�ɾP���ۤv
        if (other.gameObject.CompareTag("Player"))
        {
           
          
            Destroy(gameObject);
        }
    }
}
