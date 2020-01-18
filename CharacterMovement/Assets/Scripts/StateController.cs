using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;

    MovementController mController; // movement controller
    SlideScript sController; // slide controller;
    CrouchScript cController; // crouch controller;
    GlideScript gController; // Glide controller
    FlyScript fController; // Fly controller;

    bool isCrouched;
    bool isGrounded;
    bool isSliding;
    bool isGliding;
    bool isFlying;

    Vector3 velocity;

    private void Awake()
    {
        if (!mController)
            mController = GetComponent<MovementController>();
        if (!sController)
            sController = GetComponent<SlideScript>();
        if (!cController)
            cController = GetComponent<CrouchScript>();
        if (!gController)
            gController = GetComponent<GlideScript>();
        if (!fController)
            fController = GetComponent<FlyScript>();
    }

    private void Update()
    {
        isCrouched = cController.IsCrouched();
        isGrounded = mController.IsGrounded;
        isSliding = sController.IsSliding();
        isGliding = gController.IsGliding();
        isFlying = fController.IsFlying();
        velocity = mController.GetVelocity();

        if (isGrounded)
        {
            if (isSliding)
            {
                playerState = PlayerState.Sliding;
            }
            else if (!isSliding)
            {
                if (isCrouched)
                {
                    playerState = PlayerState.Crouching;
                }
                else if (!isCrouched)
                {
                    playerState = PlayerState.Grounded;
                }
            }
        }
        else if (!isGrounded)
        {
            if (isGliding)
            {
                playerState = PlayerState.Gliding;
            }
            else if (!isGliding)
            {
                if (isFlying)
                {
                    playerState = PlayerState.Flying;
                }
                else if (!isFlying)
                {
                    if (velocity.y > 0.01)
                    {
                        playerState = PlayerState.Jumping;
                    }
                    else if (velocity.y < -0.01)
                    {
                        playerState = PlayerState.Falling;
                    }
                }
            }
        }
    }

    public PlayerState GetState()
    {
        return playerState;
    }

}
public enum PlayerState
{
    Grounded,
    Falling,
    Crouching,
    Sliding,
    Gliding,
    Jumping,
    Flying
}