using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public Transform PlayerTransform;
    public float maxTime = 1.0f;
    public float maxDistance=1.0f;
    float timer = 0.0f;

    NavMeshAgent agent;
  
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0.0f)
        {
            float sqDistance = (PlayerTransform.position - agent.destination).sqrMagnitude;
            if(sqDistance>maxDistance*maxDistance)
            {
                agent.destination = PlayerTransform.position;
            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
