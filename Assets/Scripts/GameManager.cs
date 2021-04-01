using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public GameObject EnnemyPrefab;


    public RbCharacterMovements playerMovements;
    public Ennemy police;
    public AudioSource source;

    bool isGameOver;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null)
            return;

        singleton = this;
        source = GetComponent<AudioSource>();

        InvokeRepeating("SpawnEnnemy", 15f, 15f);
    }

    void SpawnEnnemy()
    {
        if (!isGameOver)
            Instantiate(EnnemyPrefab, new Vector3(3f, 0.6f, 9f), EnnemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        //Empêcher les mouvements du joueur
        playerMovements.enabled = false;

        //Empêcher les mouvements du NPC
        Ennemy[] polices = FindObjectsOfType<Ennemy>();
        foreach(Ennemy p in polices)
        { 
            police.enabled = false;
            police.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }

        //Message de fin de jeu

        Debug.Log($"Fin du jeu en {Time.time}s");
        source.Play();

    }
}
