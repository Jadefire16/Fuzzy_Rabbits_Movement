using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour
{
    [SerializeField] float startFlyTime = 10f;
    float flyTime;
    bool isFlying = false;

    MovementController mController;
    StateController state;
    

    private void Start()
    {
        if (!mController)
            mController = GetComponent<MovementController>();
        if (!state)
            state = GetComponent<StateController>();

        flyTime = startFlyTime;
    }

    private void Update()
    {
        
        if (Input.GetMouseButton(1) && !isFlying)
        {
            switch (state.GetState())
            {
                case PlayerState.Grounded:
                    break;
                case PlayerState.Falling:
                    StartCoroutine(Fly(flyTime));
                    break;
                case PlayerState.Crouching:
                    break;
                case PlayerState.Sliding:
                    break;
                case PlayerState.Gliding:
                    break;
                case PlayerState.Jumping:
                    StartCoroutine(Fly(flyTime));
                    break;
                case PlayerState.Flying:
                    break;
                default:
                    break;
            }
        }
        else { return; }
    }

    IEnumerator Fly( float flyTimer )
    {
        float i = 0f;
        while (i < flyTimer)
        {
            flyTimer += Time.deltaTime;
            isFlying = true;
            mController.SetGravMult(0.01f);
            yield return null;
        }
        isFlying = false;
    }

    public bool IsFlying()
    {
        bool x = isFlying;
        return x;
    }
}
