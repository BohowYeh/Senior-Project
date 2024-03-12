using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RaycastWeapon : MonoBehaviour
{
    public Transform raycastOrigin;
    public GameObject MuzzleFlash;//�s�y�j�f�K
    public AudioSource gunshot;
    public AudioClip singleShot;
   


    //RaycastWeapon ray;
    //RaycastHit hitInfo;

    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MuzzleFlash.SetActive(true);//���ͺj�f�K
            StartCoroutine(wait());//�}�l�U�誺�j�f�K�����p�ɾ�Ū��
            gunshot.PlayOneShot(singleShot);
        }
         
    }

    //�j�f�K�p�ɾ�
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.05f);
        MuzzleFlash.SetActive(false);
    }





}
