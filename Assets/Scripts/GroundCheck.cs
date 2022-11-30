using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public PlayerController playercontroller;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != playercontroller.gameObject) playercontroller.SetGrounded(true);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject != playercontroller.gameObject) playercontroller.SetGrounded(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject != playercontroller.gameObject) playercontroller.SetGrounded(false);
    }
}
