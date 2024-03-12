using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BulletMove : MonoBehaviour
{
    public Vector3 hitPoint;

    public GameObject blood;

    public int speed;

    public float damege=450;

    public ParticleSystem hitEffect;

    //public AudioSource myShot;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * speed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    void OnCollisionEnter(Collision col)
    {

        Debug.Log("7777");
        if (col.gameObject.tag == "Enemy")
        {

            //GameObject newBlood = Instantiate(blood, this.transform.position, this.transform.rotation);
            //newBlood.transform.parent = col.transform;
            Debug.Log("9999");
            var hitBox=col.gameObject.GetComponent<HitBox>();
            Debug.Log("8888");
            if (hitBox)
            {
                Debug.Log("GOGOGO");
                hitBox.OnRaycastHit(this,new Vector3(3,0,0));
            }

            Destroy(this.gameObject);
        }
        else
        {
            //Instantiate(HitMetel, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        /*
        hitEffect.transform.position = hitPoint.point;
        hitEffect.transform.forword = hitPoint.normal;
        hitEffect.Emit(1);
        */
        
       




        Destroy(this.gameObject);
    }
}