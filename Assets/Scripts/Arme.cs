using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : MonoBehaviour
{
    public Transform barrel;
    public LineRenderer bulletTrail;

    public float explosionRadius;
    public float explosionForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
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

        }

        else
        {
            //Bullet Trail (point B Raté)
            bulletTrail.SetPosition(1, barrel.position + barrel.forward * 50f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(barrel.position, barrel.forward * 50f);
    }
}
