using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlideScript : MonoBehaviour
{
    Rigidbody rb;
    CrouchScript crouch;
    MovementController controller;
    CharacterController charController;
    Transform castPoint;
    [SerializeField] List<PhysicMaterial> physMat = new List<PhysicMaterial>();
    [SerializeField] private float slideForce = 4.5f; // used to continue momentum when entering slide mode 4.5f seeems good

    public LayerMask antiSlideMask;

    bool isSliding = false;

    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
        if (!crouch)
            crouch = GetComponent<CrouchScript>();
        if (!controller)
            controller = GetComponent<MovementController>();
        if (!charController)
            charController = GetComponent<CharacterController>();
        if (!castPoint)
            castPoint = controller.groundCheck;
        rb.useGravity = false;
        rb.detectCollisions = false;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && controller.IsGrounded)
        {
            EnableSlide();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableSlide();
        }

        Ray ray = new Ray(castPoint.position, -castPoint.up);
        if (Physics.Raycast(ray, out RaycastHit info, 0.25f, antiSlideMask, QueryTriggerInteraction.Ignore))
        {
            DisableSlide();
        }


    }

    void DisableSlide()
    {
        if (!rb)
            return;
        controller.enabled = true;
        crouch.enabled = true;
        charController.enabled = true;
        crouch._collider.material = physMat[0];
        isSliding = false;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        rb.useGravity = false;
        rb.Sleep();
    }

    void EnableSlide()
    {
        if (!rb)
            return;
        crouch._collider.material = physMat[1];
        controller.enabled = false;
        crouch.enabled = false;
        charController.enabled = false;
        isSliding = true;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.WakeUp();

        rb.AddForce(transform.forward * slideForce, ForceMode.Impulse);
    }

}
