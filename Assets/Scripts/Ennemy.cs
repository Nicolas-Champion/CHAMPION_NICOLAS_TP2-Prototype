using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    
    public Transform eyes;

    Transform playerHead;

    float speedWalking = 1f;
    float speedRunning = 2.5f;
    float lastDetectionTime = -100f;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    AudioSource source;

    //L'ennemie est-il occupé? 
    bool isEnnemyBusy;

    void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        source = GetComponent<AudioSource>();

    }


    void Start()
    {
        playerHead = FindObjectOfType<RbCharacterMovements>().head;
        InvokeRepeating("DelayedUpdate", 1f, 1f);

    }
    // Update is called once per frame
    void Update()
    {
        if (!isEnnemyBusy)
            StartCoroutine(Patrol(GetRandomDest(), speedWalking));

        //Arrestation?
        if (Vector3.Distance(transform.position, playerHead.position) < 1f)
        {
            GameManager.singleton.GameOver();
        }
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

        //Déplacement vers dest
        navMeshAgent.SetDestination(destination);

        //Rien d'autre tant que pas arrivé à dest
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.5)
        {
            yield return null;
        }

        //pause rendu à dest
        yield return new WaitForSeconds(Random.Range(3f, 6f));


        //démarre une nouvelle patrouille
        isEnnemyBusy = false;


    }

    void TryDetectPlayer()
    {
        //Créer un rayon
        RaycastHit hit;
        //S'il intersecte ou est obstrué par un collider autre que le personnage
        if (Physics.Linecast(eyes.position, playerHead.position, out hit))

        {
            //Vérifier si c'est le joueur
            if (hit.collider.CompareTag("Player"))
            {
                //Poursuite du joueur
                StopAllCoroutines();
                StartCoroutine(Patrol(playerHead.position, speedRunning));

                //SFX
                if (Time.time > lastDetectionTime + 10f)
                {
                    source.Play();
                    lastDetectionTime = Time.time;
                }
            }
        }
    }
}
