using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPT2 : MonoBehaviour
{
    bool CPT = false;
    float CPTTimer = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CPT)
        {
            if (CPTTimer <= 0)
            {

                Destroy(gameObject);

            }
            else
            {
                CPTTimer -= Time.deltaTime;
            }
        }
    }
    void OnEnable()
    {
        CPT = true;
        CPTTimer = 5.0f;
    }
}
