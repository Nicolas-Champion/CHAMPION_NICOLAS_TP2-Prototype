using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RbCharacterMovements : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 1f;
    public float speedWalking;
    public float speedRunning;

    // Transform de la position des pieds
    public Transform feetPosition;

    private float inputVertical;
    private float inputHorizontal;
    private Animator animatorVanguard;
    private float LerpPercent = 0.08f;
    private float animationSpeed = 1f;
    
    bool isMoving;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool isGrounded = true;

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

    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPosition.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        // Vérifier les inputs du joueur
        // Vertical (W, S et Joystick avant/arrière)
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal (A, D et Joystick gauche/droite)
        inputHorizontal = Input.GetAxis("Horizontal");

        // Vecteur de mouvements (Avant/arrière + Gauche/Droite)
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;

        // Rotation du personnage
        Rotate();

        // Sauter
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();


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



    }

    void Rotate()
    {
        Vector3 rotPlayer = transform.rotation.eulerAngles;

        // Le player tourne en fonction de la position de la souris (y seulement)
        rotPlayer.y += Input.GetAxis("Mouse X");

        // Appliquer la rotation rotPlayer dans la rotation du Transform (Quaternion)
        transform.rotation = Quaternion.Euler(rotPlayer);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
