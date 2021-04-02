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

    void Awake()
    {
        source = GetComponent<AudioSource>();
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
        
        //Créer un rayon qui va vers l'avant du pistolet
        Ray pistolRay = new Ray(barrel.position, barrel.forward);
        RaycastHit hit;

        //Impact du rayon?
        if (Physics.Raycast(pistolRay, out hit, 50f))
        {
            //mini-Explosion
            Explosion explosion = new Explosion(explosionForce, hit.point, explosionRadius, 0.5f);

        }

        
    }

}
