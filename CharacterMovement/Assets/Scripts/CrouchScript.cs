using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class CrouchScript : MonoBehaviour
{
    public CharacterController _controller;
    public CapsuleCollider _collider;

    private bool isCrouched = false;
    private bool transition;

    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float smoothSpeed = 2f;
    [SerializeField] private float smoothDuration = 5f;

    public float scaleMultiplier = 0.6f;

    private void Awake()
    {
        if (!_controller)
            _controller = GetComponent<CharacterController>();
        if (!_collider)
            _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        minScale = (transform.localScale.y * scaleMultiplier);
        maxScale = transform.localScale.y;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (isCrouched && !transition)
            {
                transition = true;
                isCrouched = false;
                StartCoroutine(Crouch(minScale, maxScale, smoothDuration));
            }

            else if (!isCrouched && !transition)
            {
                transition = true;
                isCrouched = true;
                StartCoroutine(Crouch(maxScale, minScale, smoothDuration));
            }
        }
    }

    IEnumerator Crouch(float a, float b, float time)
    {
        float i = 0f;
        float rate = (1.0f / time) * smoothSpeed;
        Vector3 scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            scale.y = Mathf.Lerp(a, b, i);
            transform.localScale = scale;
            yield return null;
        }
        transition = false;
    }

    public bool IsCrouched()
    {
        if (isCrouched) { return true; } else { return false; }
    }
}
