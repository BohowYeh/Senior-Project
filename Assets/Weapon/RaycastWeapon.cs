using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RaycastWeapon : MonoBehaviour
{
    public Transform raycastOrigin;
    public GameObject MuzzleFlash;//製造槍口焰
    public AudioSource gunshot;
    public AudioClip singleShot;
   


    //RaycastWeapon ray;
    //RaycastHit hitInfo;

    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MuzzleFlash.SetActive(true);//產生槍口焰
            StartCoroutine(wait());//開始下方的槍口焰結束計時器讀秒
            gunshot.PlayOneShot(singleShot);
        }
         
    }

    //槍口焰計時器
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.05f);
        MuzzleFlash.SetActive(false);
    }





}
