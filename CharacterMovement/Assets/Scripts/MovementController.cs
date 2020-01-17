using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to ensure that the character always has the required components!

[RequireComponent(typeof(CharacterController))] // Ensures we have a character controller component
public class MovementController : MonoBehaviour
{
    #region Variables

    [SerializeField] private CharacterController controller;
    StateController state;

    public Transform groundCheck; // used to cast a physics overlap sphere to test if we're on ground
    public LayerMask groundMask; // used to check what layers are considered ground

    public float startSpeed = 4f; // Allows us to set starting values that wont affect
    public float startJumpHeight = 1.5f; // our actual values after startup
    public float gravMin;
    public float gravMax; // Use these to provide a better feeling jump (These should always remain above 1 unless you want to float/hover


    private float speed; // Our players actual speed value
    private float jumpHeight; // Our players actual jumpHeight value that we'll use

    [SerializeField] private const float gCheckRadius = 0.3f; // Defines our ground check spheres' radius
    [SerializeField] private const float gravity = -9.81f; // This wont ever change its a constant value
    [SerializeField] private float gravMultiplier = 1f; // since gravity is constant (we cant change it) we can use this multiplier to scale it
    private float baseGravMultiplier; // used to always set gravMultiplier back to it's default value

    private Vector3 velocity; // Allows us to control and manipulate our Velocity

    [SerializeField] private bool isGrounded; // To confirm whether we're grounded;
    private float x, z; // Creates two floats named x & z while saving space

    #endregion Variables

    // I rewrote the class and organized everything with comments
    // since I couldn't find the original project

    // If you cant find something Open one of the labeled region tabs to further 
    // expand the code inside of the region

    void Awake()
    {
        if (!controller) // Making sure we have a reference to our controller component
            controller = GetComponent<CharacterController>();
        if (!state)
            state = GetComponent<StateController>();
    }

    private void Start()
    {
        speed = startSpeed;
        jumpHeight = startJumpHeight;
        baseGravMultiplier = gravMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement

        isGrounded = Physics.CheckSphere(groundCheck.position, gCheckRadius, groundMask); // Creates a check sphere which allows us to see if we've collided with anything with the specified Layer

        if (isGrounded)
        {
            if(velocity.y < 0)
            {
                gravMultiplier = baseGravMultiplier;
                velocity.y = -2f; // Ensures we're always putting a bit of force on the player downwards to keep it on the ground
            }
        }
        else if (!isGrounded)
        {
            if (state.GetState() != PlayerState.Gliding)
            {
                if (velocity.y > 0.05)
                {
                    gravMultiplier = gravMin;
                }
                else if (velocity.y < -0.05)
                {
                    gravMultiplier = gravMax;
                }
            }
        }


        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * (X * 0.8f) + transform.forward * Z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravMultiplier)); // -2f negates our downwards force that we were applying above

        velocity.y += (gravity * gravMultiplier) * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        #endregion Movement
    }

    public Vector3 GetVelocity()
    {
        Vector3 X = velocity;
        return X;
    }

    public void SetGravMult(float value)
    {
        gravMultiplier = value;
    }
    public float GetGravMult()
    {
        float x = gravMultiplier;
        return x;
    }

    #region Get&Set Variables
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public float X { get => x; set => x = value; }
    public float Z { get => z; set => z = value; }

    #endregion Get&Set Variables
}

