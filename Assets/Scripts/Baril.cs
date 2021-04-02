using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baril : MonoBehaviour
{
    public float radius;
    public float force;
    public int PV = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si le baril a été touché par une balle il explose dans 3 secondes
        if (PV < 1)
            Invoke("Explode", 3f);
    }

    void Explode()
    {
        //Explosion
        Explosion explosion = new Explosion(force, transform.position, radius, 0.5f);
        //Détruire la bombe
        Destroy(gameObject);

    }
}
