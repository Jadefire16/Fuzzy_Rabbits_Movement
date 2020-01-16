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
    [SerializeField] List<PhysicMaterial> physMat = new List<PhysicMaterial>();

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
        rb.useGravity = false;
        rb.detectCollisions = false;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            crouch._collider.material = physMat[1];

            controller.enabled = false;
            crouch.enabled = false;
            charController.enabled = false;
            EnableRB();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            crouch._collider.material = physMat[0];

            DisableRB();
            controller.enabled = true;
            crouch.enabled = true;
            charController.enabled = true;
        }

    }

    void DisableRB()
    {
        if (!rb)
            return;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        rb.useGravity = false;
        rb.Sleep();
    }

    void EnableRB()
    {
        if (!rb)
            return;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.WakeUp();
    }

}
