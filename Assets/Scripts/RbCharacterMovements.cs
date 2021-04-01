using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbCharacterMovements : MonoBehaviour
{
    float speed = 0.1f;
    public float jumpHeight = 1f;
    public float speedWalking;
    public float speedRunning;


    // Transform de la position des pieds
    public Transform feetPosition;

    private float inputVertical;
    private float inputHorizontal;
    private float animationSpeed = 1f;
    private float LerpPercent = 0.08f;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool isGrounded = true;

    private Animator animatorVanguard;

    bool isMoving;

    // Start is called before the first frame update
    void Awake()
    {
        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();
        animatorVanguard = GetComponent<Animator>();
    }

    void Start()
    {
        // Modifier le layer pour que l'objet puisse uniquement sauté quand il touche le sol (il ne sera plus sur le layer Default)
        gameObject.layer = 3;

        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();

        // Masse du Rigidbody
        rb.mass = 8f;

        // Geler la rotation physique
        rb.freezeRotation = true;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPosition.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        // Vérifier les inputs du joueur
        // Vertical (W, S et Joystick avant/arrière)
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal (A, D et Joystick gauche/droite)
        inputHorizontal = Input.GetAxis("Horizontal");

        //Vérifier deadzones

        isMoving = Mathf.Abs(inputHorizontal) + Mathf.Abs(inputVertical) > 0f;

        animatorVanguard.SetBool("IsMoving", isMoving);
        //Animation  ------------------------------------
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Courir
            animationSpeed = Mathf.Lerp(animationSpeed, 2f, LerpPercent);
            speed = Mathf.Lerp(speed, speedRunning, LerpPercent);
        }
        else
        {
            animationSpeed = Mathf.Lerp(animationSpeed, 1f, LerpPercent);
            speed = Mathf.Lerp(speed, speedWalking, LerpPercent);
        }

        animatorVanguard.SetFloat("Horizontal", inputHorizontal * animationSpeed);
        animatorVanguard.SetFloat("Vertical", inputVertical * animationSpeed);
        //-----------------------------------------------

        // Vecteur de mouvements (Avant/arrière + Gauche/Droite)
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;

        // Sauter
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            animatorVanguard.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            
        }

        //Shoot
        if (Input.GetMouseButtonDown(0))
        {
            animatorVanguard.SetTrigger("Shoot");
        }
    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
    }
}
