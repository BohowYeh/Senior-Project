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
        //當碰到玩家時銷毀自己
        if (other.gameObject.CompareTag("Player"))
        {
            Bob.SetActive(true);
            source.PlayOneShot(clip);
            Destroy(gameObject);
        }
    }
}
