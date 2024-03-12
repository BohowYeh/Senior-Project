using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bob : MonoBehaviour
{
    bool Bob = false;
    float BobTimer = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Bob)
        {
            if (BobTimer <= 0)
            {

                Destroy(gameObject);

            }
            else
            {
                BobTimer -= Time.deltaTime;
            }
        }
    }
    void OnEnable()
    {
        Bob = true;
        BobTimer = 5.0f;
    }
}
