using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public GameObject Bob;
    void OnTriggerEnter(Collider other)
    {
        //��I�쪱�a�ɾP���ۤv
        if (other.gameObject.CompareTag("Player"))
        {
            Bob.SetActive(true);
            source.PlayOneShot(clip);
            Destroy(gameObject);
        }
    }
}
