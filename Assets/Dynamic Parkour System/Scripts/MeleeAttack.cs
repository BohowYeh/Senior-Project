using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject collider;
    public Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;
    float SheathTimer = 3.0f;
    float SheathDelayTimer = 1.0f;
    bool SheathCount = false;
    bool SheathDelayCount = false;
    public GameObject Sword;
    public GameObject SwordBack;
    public bool canDamage;
    public Collider cd;
    public AudioSource source;
    public AudioClip swing1;
    public AudioClip swing2;

    void Start()
    {
       cd= Sword.GetComponent<Collider>();
        cd.enabled = false;
        anim = GetComponent<Animator>();
        anim.SetBool("Draw", false);
        Sword.SetActive(false);
        SwordBack.SetActive(true);
    }

  
    void Update()
    {
        if (SheathCount)
        {
            if (SheathTimer <= 0)
            {
                anim.SetBool("Sheath", true);
                anim.SetBool("Draw", true);            
                SheathCount = false;
                SheathDelayCount = true;
                SheathDelayTimer = 0.5f;
            }
            else
            {
                SheathTimer -= Time.deltaTime;
            }
        }

        if(SheathDelayCount)
        {
            if (SheathDelayTimer <= 0)
            {
                Debug.Log("WORKING2");
                Sword.SetActive(false);
                SwordBack.SetActive(true);
                SheathDelayCount = false;
            }
            else
            {
                SheathDelayTimer -= Time.deltaTime;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Sheath"))
        {
            anim.SetBool("Sheath", false);//�����ʵe

        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);//�����ʵe
            cd.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            cd.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            cd.enabled = false;
            noOfClicks = 0;
        }

        if(Time.time - lastClickedTime > maxComboDelay)//�I������j��̤jCombo����ɶ�
        {
            noOfClicks = 0;//�k�s�I����                
        }
        if(Time.time > nextFireTime)//�I������bCombo����ɶ���
        {
            if (Input.GetMouseButtonDown(0))//�p���I������U���{��
            {            
                OnClick();             
                SheathCount = true;
                SheathTimer = 3.0f;
               
            }         
        }
    }

    void OnClick()
    {
        Sword.SetActive(true);
        SwordBack.SetActive(false);

        lastClickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            cd.enabled = true;
            anim.SetBool("hit1", true);
            source.PlayOneShot(swing1);


        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);//�T�{�ƭȬO�_�b�̤j�M�̤p�d��

        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))//�I������>2�� �B�I������>0.7�� �ثe����ʵe�S�Ohit1���ɭ�
        {
           // anim.SetBool("hit1", false);//����hit1�ʵe
            anim.SetBool("hit2", true);//��hit2�ʵe
            cd.enabled = true;
            anim.SetBool("Draw", false);
            source.PlayOneShot(swing1);
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            //anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
            cd.enabled = true;
            source.PlayOneShot(swing2);
        }


    }
}
