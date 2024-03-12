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
        originalColor = skinnedMeshRenderer.material.color;  //用來做hit effect
        currentHealth = maxHealth;                                           //血量重設

        foreach (var rigidBody in rigidBodies) //在有rigidbody的所有子物件 生成hitBox script物件並統一為this，共用血量
        {
           HitBox hitBox= rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.gameObject.tag = "Enemy";
            hitBox.Health = this; 
        }
    }

    // Update is called once per frame
    public void TakeDamage( float amount,Vector3 direction)    //受到傷害時呼叫 放在 HitBox Class的OnRaycastHit方法
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if(currentHealth<=0.0f)
        {
            Die(direction);
        } 
        blinkTimer = blinkDuration;
    }

    private void Die( Vector3 direction)             //敵人死亡,延子彈方向向上擊飛 ,關閉ai、物理引擎和血條
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
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);   // 用來製造被打擊效果，被打擊後亮度逐禎下降，直到lerp=0
        float intensity = (lerp * blinkIntensity)+1.0f;
        Debug.Log(lerp);
        

        skinnedMeshRenderer.materials[0].color = Color.white * intensity;     //打擊效果    把所有Ai敵人模型物件都變白
        skinnedMeshRenderer.materials[1].color = Color.white * intensity;
        skinnedMeshRenderer.materials[2].color = Color.white * intensity;
      if(blinkTimer<0)
        {
            skinnedMeshRenderer.materials[0].color = originalColor;   //回復正常
            skinnedMeshRenderer.materials[1].color = originalColor;
            skinnedMeshRenderer.materials[2].color = originalColor; 
    
        }
      if(test>0)
        TakeDamage(test, new Vector3(1, 1, 0));


    }
   
} 
