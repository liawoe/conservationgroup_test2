using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerXR : MonoBehaviour
{
    public Rigidbody rb;
    public bool grounded;
    public GameObject camHolder;
    public float speed, sensitivity, maxForce, jumpForce;
    private Vector2 move, look;
    private float lookRotation;

    internal void SetGrounded(bool v)
    {
        grounded = v;
    }

    public Vector2 GetLookingAngle()
    {
        Vector2 lookingAngle;
        lookingAngle.x = camHolder.gameObject.transform.rotation.eulerAngles.x;
        lookingAngle.y = transform.rotation.eulerAngles.y;
        return lookingAngle;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        Vector3 jumpforces = Vector3.zero;

        if (grounded)
        {
            jumpforces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpforces, ForceMode.VelocityChange);
    }

    void Move()
    {
        //Find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate forces
        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //Limit force
        _ = Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * look.x * sensitivity);

        //Look
        lookRotation += -look.y * sensitivity;
        lookRotation = Mathf.Clamp(lookRotation, min: -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Look();
    }
}
