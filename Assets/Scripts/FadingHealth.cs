using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingHealth : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    [Header("Player Health")]
    public float currentPlayerHealth = 100.0f;
    [SerializeField] private float maxPlayerHealth = 100.0f;
    [SerializeField] private int regenRate = 1;
    private bool canRegen = false;
 
    [Header("Add the Splatter image here")]
    [SerializeField] private Image redSplatterImage = null;

    [Header("Heal Timer")]
    [SerializeField] private float healCooldown = 3.0f;
    [SerializeField] private float maxHealCooldown = 3.0f;
    [SerializeField] private bool startCooldown = false;

    [Header("Hurt Image Flash")]
    [SerializeField] private Image hurtImage = null;
    [SerializeField] private float hurtTimer = 0.1f;

    [Header("Audio Name")]
    [SerializeField] private AudioClip hurtAudio = null;
    private AudioSource healthAudioSource;

    private void Start()
    {
        healthAudioSource = GetComponent<AudioSource>();
    }

   void UpdateHealth()
    {
        Color splatterAlpha = redSplatterImage.color;
        splatterAlpha.a = 1 - (currentPlayerHealth / maxPlayerHealth);
        redSplatterImage.color = splatterAlpha;
    }

    IEnumerator HurtFlash()
    {
        currentPlayerHealth-= 5;
        hurtImage.enabled = true;
        healthAudioSource.PlayOneShot(hurtAudio);
        yield return new WaitForSeconds(hurtTimer);
        hurtImage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sword")
        {
            source.PlayOneShot(clip);
            TakeDamage();
           
}
    }
    public void TakeDamage()
    {
        if(currentPlayerHealth >= 0)
        {
            canRegen = false; 
            StartCoroutine(HurtFlash());
            UpdateHealth();
            healCooldown = maxHealCooldown;
            startCooldown = true;          
        }
    }

    private void Update()
    {
        if(startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if(healCooldown <= 0)
            {
                canRegen = true;
                startCooldown = false;
            }
        }

        if(canRegen)
        {
            if(currentPlayerHealth <= maxPlayerHealth - 0.01)
            {
                currentPlayerHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            }
            else
            {
                currentPlayerHealth = maxPlayerHealth;
                healCooldown = maxHealCooldown;
                canRegen = false;
            }
        }
    }
}

