using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    
    public Transform eyes;


    Transform playerHead;
    Rigidbody[] ragdollRbs;

    float speedWalking = 1f;
    float speedRunning = 2.5f;
    private float LerpPercent = 0.08f;
    //float lastDetectionTime = -100f;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    AudioSource source;

    private Animator animatorVanguard;



    //L'ennemie est-il occupé? 
    bool isEnnemyBusy;

    void Awake()
    {
        //Instanciation des components
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        source = GetComponent<AudioSource>();
        ragdollRbs = GetComponentsInChildren<Rigidbody>();

        animatorVanguard = GetComponent<Animator>();


    }


    void Start()
    {
        playerHead = FindObjectOfType<RbCharacterMovements>().head;
        InvokeRepeating("DelayedUpdate", 1f, 1f);

    }
    // Update is called once per frame
    void Update()
    {

        //Si le rigid body est désactivé, désactiver le navMeshAgent
        foreach (Rigidbody rb in ragdollRbs)
        {
            if (!rb.isKinematic)
                navMeshAgent.enabled = false;
        }

        if (!isEnnemyBusy && navMeshAgent.enabled)
            StartCoroutine(Patrol(GetRandomDest(), speedWalking));


        RaycastHit hit;
        if (Physics.Linecast(eyes.position, playerHead.position, out hit))
        {
            ShootingAnimation();
        }

        



    }

    void ShootingAnimation()
    {
        
        animatorVanguard.SetTrigger("Shoot");
    }

    void DelayedUpdate()
    {
        TryDetectPlayer();
    }

    Vector3 GetRandomDest()
    {
        float xLimit = Random.Range(-11f, 7f);
        float zLimit = Random.Range(-11f, 11f);


        return new Vector3(xLimit, 0f, zLimit);
    }

    IEnumerator Patrol(Vector3 destination, float speed)
    {
        isEnnemyBusy = true;

        navMeshAgent.speed = speed;

        float animationSpeed = 1f;    
        //Déplacement vers dest
        navMeshAgent.SetDestination(destination);
        
        //Rien d'autre tant que pas arrivé à dest
        if (navMeshAgent.enabled)
        {
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.5)
            {
                yield return null;
            }
        }
        //pause rendu à dest
        yield return new WaitForSeconds(Random.Range(3f, 6f));


        //démarre une nouvelle patrouille
        isEnnemyBusy = false;

        animationSpeed = Mathf.Lerp(animationSpeed, 1f, LerpPercent);
        speed = Mathf.Lerp(speed, speedWalking, LerpPercent);

        animatorVanguard.SetFloat("Horizontal", speed * animationSpeed);
        animatorVanguard.SetFloat("Vertical", speed * animationSpeed);
    }

    void TryDetectPlayer()
    {
        //Créer un rayon
        RaycastHit hit;
        //S'il intersecte ou est obstrué par un collider autre que le personnage
        if (Physics.Linecast(eyes.position, playerHead.position, out hit) && navMeshAgent.enabled)

        {
            //Vérifier si c'est le joueur
            if (hit.collider.CompareTag("Player"))
            {
                //Poursuite du joueur
                StopAllCoroutines();
                StartCoroutine(Patrol(playerHead.position, speedRunning));



            }
        }
    }
}
