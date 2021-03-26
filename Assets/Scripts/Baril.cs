using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baril : MonoBehaviour
{
    public float radius;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        //Explosion
        Explosion explosion = new Explosion(force, transform.position, radius, 0.5f);
        //Détruire la bombe
        Destroy(gameObject);

    }
}
