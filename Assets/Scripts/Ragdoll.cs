using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
   public Rigidbody[] rigidBodies;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
      
        animator = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    // Update is called once per frame
   public void DeactivateRagdoll()                //使所有子物件的rigidbody變為Kinematic 開啟動畫
    {
        foreach(var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        
        }
        animator.enabled = true;
    }

    public void ActivateRagdoll()    //使所有子物件的rigidbody變為非Kinematic
    {
        foreach(var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
         
        }
        animator.enabled = false;
    }
    public void ApplyForce(Vector3 force )
    {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
     }
}
