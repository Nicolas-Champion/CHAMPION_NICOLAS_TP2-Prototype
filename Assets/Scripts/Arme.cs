using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : MonoBehaviour
{
    Audio audio = new Audio();
    AudioSource source;
    public Transform barrel;
    public LineRenderer bulletTrail;
    public GameObject particule;

    public float explosionRadius;
    public float explosionForce;

    
    void Awake()
    {
        source = GetComponent<AudioSource>();
        particule.SetActive(false);

    }

    void Start()
    {
        //Envoyer une delayed Update pour refermer le particle system
        InvokeRepeating("DelayedUpdate", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //détection du click gauche
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            audio.AudioActivation(GetComponent<AudioSource>());

            
        }
        
    }

    void Shoot()
    {
        //Bullet Trail(pointA)
        bulletTrail.SetPosition(0, barrel.position);

        //Créer un rayon qui va vers l'avant du pistolet
        Ray pistolRay = new Ray(barrel.position, barrel.forward);
        RaycastHit hit;

        //Impact du rayon?
        if (Physics.Raycast(pistolRay, out hit, 50f))
        {
            //bulletTrail (pointB)
            bulletTrail.SetPosition(1, hit.point);

            //mini-Explosion
            Explosion explosion = new Explosion(explosionForce, hit.point, explosionRadius, 0.5f);
            
            //Mettre la position des particules au point d'impact
            particule.transform.position = hit.point;
            
            //Activer les particule pour avoir un burst
            particule.SetActive(true);
        }

        else
        {
            //Bullet Trail (point B Raté)
            bulletTrail.SetPosition(1, barrel.position + barrel.forward * 50f);
        }

        //Si le tag est celui de barrel, lui enlever un PV pour qu'il explose (pas trouvé comment)
        //if (hit.collider.CompareTag("Barrel"))
        //{
           
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(barrel.position, barrel.forward * 50f);
    }

    //Fermeture du particle system
    void DelayedUpdate()
    {
        particule.SetActive(false);
        
    }
}
