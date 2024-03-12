using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPT1 : MonoBehaviour
{
    bool CPT = false;
    float CPTTimer = 10.0f;
    public GameObject CP;
    // Start is called before the first frame update
    void Start()
    {
        CPT = true;
        CPTTimer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (CPT)
        {
            if (CPTTimer <= 0)
            {
                CP.SetActive(true);
               

            }
            else
            {
                CPTTimer -= Time.deltaTime;
            }
        }
    }
    
}
