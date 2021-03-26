using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] ragdollRbs;
    Animator animatorVanguard;

    bool isDead = false;

    public bool debugKill;

    // Start is called before the first frame update
    void Awake()
    {
        //Lister tous les Rbs
        ragdollRbs = GetComponentsInChildren<Rigidbody>();

        animatorVanguard = GetComponent<Animator>();
        //Désactiver le ragdoll
        ToggleRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(debugKill)
        {
            Die();
            debugKill = false;
        }
    }

    public void Die()
    {
        if (isDead)
            return;
        isDead = true;
        
        ToggleRagdoll(true);    
    }

    void ToggleRagdoll(bool value)
    {

        //Mettre le kinematic à contraire de value
        foreach (Rigidbody rb in ragdollRbs)
        {
            rb.isKinematic = !value;
        }

        //Animator
        animatorVanguard.enabled = !value;
    }
}
