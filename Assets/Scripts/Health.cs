using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    NavMeshAgent agent;
 
    public float maxHealth=100f;
    public float currentHealth;
    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer;
    public float dieForce,test;
    
    SkinnedMeshRenderer skinnedMeshRenderer;
    Ragdoll ragdoll;
    UIHealthBar healthBar;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        ragdoll = GetComponent<Ragdoll>();                                
        healthBar = GetComponentInChildren<UIHealthBar>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();        
        agent = GetComponent<NavMeshAgent>();
        originalColor = skinnedMeshRenderer.material.color;  //�ΨӰ�hit effect
        currentHealth = maxHealth;                                           //��q���]

        foreach (var rigidBody in rigidBodies) //�b��rigidbody���Ҧ��l���� �ͦ�hitBox script����òΤ@��this�A�@�Φ�q
        {
           HitBox hitBox= rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.gameObject.tag = "Enemy";
            hitBox.Health = this; 
        }
    }

    // Update is called once per frame
    public void TakeDamage( float amount,Vector3 direction)    //����ˮ`�ɩI�s ��b HitBox Class��OnRaycastHit��k
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if(currentHealth<=0.0f)
        {
            Die(direction);
        } 
        blinkTimer = blinkDuration;
    }

    private void Die( Vector3 direction)             //�ĤH���`,���l�u��V�V�W���� ,����ai�B���z�����M���
    {
        ragdoll.ApplyForce(direction * dieForce);
        ragdoll.ActivateRagdoll();
        direction.y = 1;
       
        healthBar.gameObject.SetActive(false);
        agent.enabled = false;
        
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;                                           
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);   // �Ψӻs�y�Q�����ĪG�A�Q������G�׳v�դU���A����lerp=0
        float intensity = (lerp * blinkIntensity)+1.0f;
        Debug.Log(lerp);
        

        skinnedMeshRenderer.materials[0].color = Color.white * intensity;     //�����ĪG    ��Ҧ�Ai�ĤH�ҫ������ܥ�
        skinnedMeshRenderer.materials[1].color = Color.white * intensity;
        skinnedMeshRenderer.materials[2].color = Color.white * intensity;
      if(blinkTimer<0)
        {
            skinnedMeshRenderer.materials[0].color = originalColor;   //�^�_���`
            skinnedMeshRenderer.materials[1].color = originalColor;
            skinnedMeshRenderer.materials[2].color = originalColor; 
    
        }
      if(test>0)
        TakeDamage(test, new Vector3(1, 1, 0));


    }
   
} 
