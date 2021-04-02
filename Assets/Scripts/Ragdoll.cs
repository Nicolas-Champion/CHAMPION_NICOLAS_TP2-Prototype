using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Audio audio = new Audio();
    Rigidbody[] ragdollRbs;
    Animator animatorPerso;
    AudioSource source;


    bool isDead = false;

    public bool debugKill;

    // Start is called before the first frame update
    void Awake()
    {
        //Lister tous les Rbs
        ragdollRbs = GetComponentsInChildren<Rigidbody>();


       
        animatorPerso = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

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
        audio.AudioActivation(source);

        ToggleRagdoll(true);

        if (source.CompareTag("Player"))
            GameManager.singleton.GameOver();
    }

    void ToggleRagdoll(bool value)
    {

        //Mettre le kinematic à contraire de value
        foreach (Rigidbody rb in ragdollRbs)
        {
            rb.isKinematic = !value;
        }

        //Animator
        animatorPerso.enabled = !value;

    }
}
