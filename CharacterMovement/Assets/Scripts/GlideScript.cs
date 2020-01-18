using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideScript : MonoBehaviour
{
    bool isGliding;
    [SerializeField] float glideTimer = 3f;
    [SerializeField] float maxGlideTime;
    [SerializeField] float glideStrength = 0.4f;
    MovementController controller;
    StateController state;
    public GameObject wings;

    public float GlideTimer
    {
        get { return glideTimer; }
        set
        {
            glideTimer = value;
            if (glideTimer > maxGlideTime)
            {
                glideTimer = maxGlideTime;
            }
            else if (glideTimer < 0)
            {
                glideTimer = 0;
            }
        }
    }

    private void Awake()
    {
        if (!controller)
            controller = GetComponent<MovementController>();
        if (!state)
            state = GetComponent<StateController>();

        maxGlideTime = glideTimer;
    }

    private void Update()
    {
        if (controller.IsGrounded == true)
        {
            if (isGliding)
                StopGlide();
            GlideTimer += Time.deltaTime;
            return;
        }
        else if (controller.IsGrounded == false)
        {
            if (GlideTimer <= 0)
            {
                StopGlide();
            }
            else if (GlideTimer > 0.025 && Input.GetMouseButton(0)) //for gliding press left
            {               
                    Glide();
            }
            
        }
        if(isGliding)
            GlideTimer -= Time.deltaTime;


    }

    public void Glide()
    {
        isGliding = true;
        wings.SetActive(true);
        controller.SetGravMult(glideStrength);
    }
    public void StopGlide()
    {
        isGliding = false;
        if(wings.activeSelf) // Returns the state of the current object (Enabled or not)
            wings.SetActive(false); // Sets that state to false 
    }

    public bool IsGliding()
    {
        bool x = isGliding;
        return x;
    }
}
