using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    bool ChangeScene = false;
    float ChangeSceneTimer = 10.0f;
    bool ThanksSprite = false;
    float ThanksTimer = 10.0f;
    public GameObject fadeEffect;
    public GameObject Thanks;
    void Update()
    {
        if (ChangeScene)
        {
            if (ChangeSceneTimer <= 0)
            {

               
                SceneManager.LoadScene("Dock Thing");

            }
            else
            {
                ChangeSceneTimer -= Time.deltaTime;
            }
        }

        if (ThanksSprite)
        {
            if (ThanksTimer <= 0)
            {


                Thanks.SetActive(true);

            }
            else
            {
                ThanksTimer -= Time.deltaTime;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //當碰到玩家時銷毀自己
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeScene = true;
            fadeEffect.SetActive(true);
            ChangeSceneTimer = 15.0f;
            ThanksSprite = true;
            ThanksTimer = 7.0f;
        }
    }
}
