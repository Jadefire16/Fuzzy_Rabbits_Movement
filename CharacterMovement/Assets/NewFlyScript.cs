using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewFlyScript : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            rb.MovePosition(rb.position + transform.forward * 10 * Time.deltaTime);
    }

}
