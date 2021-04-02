using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armes : MonoBehaviour
{
    Audio audio = new Audio();
    AudioSource source;
    public Transform barrel;
    public float explosionRadius;
    public float explosionForce;
    public GameObject particule;

    public Transform eyes;

    Transform playerHead;

    UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start()
    {
        playerHead = FindObjectOfType<RbCharacterMovements>().head;

        //Envoyer une delayed Update pour refermer le particle system
        InvokeRepeating("DelayedUpdate", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //détection du joueur et tir si détecté
        if (Physics.Linecast(eyes.position, playerHead.position, out hit))
        {
            Shoot();
            audio.AudioActivation(GetComponent<AudioSource>());
        }
    }

    void Shoot()
    {

        Ray pistolRay = new Ray(barrel.position, barrel.forward);
        RaycastHit hit;

        //Impact du rayon?
        if (Physics.Raycast(pistolRay, out hit, 50f))
        {

            //mini-Explosion
            Explosion explosion = new Explosion(explosionForce, hit.point, explosionRadius, 0.5f);

            //Mettre la position des particules au point d'impact
            particule.transform.position = hit.point;

            //Activer les particule pour avoir un burst
            particule.SetActive(true);
        }

        
    }

    //Fermeture du particle system
    void DelayedUpdate()
    {
        particule.SetActive(false);

    }

}
