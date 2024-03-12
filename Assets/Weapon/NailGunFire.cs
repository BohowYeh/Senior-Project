using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;

public class NailGunFire : MonoBehaviour
{
    public float cooldownSpeed;
    public float fireRate;
    public float recoilCooldown;
    private float accuracy;
    public float maxSpreadAngle;
    public float timeTillMaxSpread;
    public GameObject bullet;
    public GameObject shootPoint;
    public GameObject MuzzleFlash;//製造槍口焰
    public ParticleSystem hitEffect;
    public AudioSource gunshot;
    public AudioClip singleShot;


   


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        cooldownSpeed += Time.deltaTime * 60f;

        if (Input.GetButton("Fire1"))
        {
          

            accuracy += Time.deltaTime * 4f;
            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                MuzzleFlash.SetActive(true);//產生槍口焰
                StartCoroutine(wait());//開始下方的槍口焰結束計時器讀秒
               
                gunshot.PlayOneShot(singleShot);
                cooldownSpeed = 0;
                recoilCooldown = 1;
            }
        }
        else
        {
            recoilCooldown -= Time.deltaTime;
            if (recoilCooldown <= 1)
            {
                accuracy = 0.0f;
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;//Raycast擊中點

        Quaternion fireRotation = Quaternion.LookRotation(transform.forward);

        float currentSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);

        fireRotation = Quaternion.RotateTowards(fireRotation, Random.rotation, Random.Range(0.0f, currentSpread));

        if (Physics.Raycast(transform.position, fireRotation * Vector3.forward, out hit, Mathf.Infinity))
        {
            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, fireRotation);
            tempBullet.GetComponent<BulletMove>().hitPoint = hit.point;

         

            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);

            
        }
    }

    //槍口焰計時器
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.05f);
        MuzzleFlash.SetActive(false);
    }
}