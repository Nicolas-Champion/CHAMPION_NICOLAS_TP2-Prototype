using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    public GameObject particule;

    // Start is called before the first frame update
    void Start()
    {
        particule.SetActive(false);

        InvokeRepeating("DelayedUpdate", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particule.SetActive(true);
        }
        
    }

    void DelayedUpdate()
    {
        particule.SetActive(false);
    }
}
