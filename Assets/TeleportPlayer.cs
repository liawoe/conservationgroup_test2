using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeleportPlayer : MonoBehaviour
{

    public GameObject teleportLocation;
    public PlayerController playerControler;
    // headTilt goes from -90, when looking straight down, to 90, when looking straight up, headTiltMin < headTiltMax
    // lookAngle goes from 0 to 360, equaling the rotation of the player object, negative values also work
    // unity's X,Z coordinate system functions in such a way, that the Z axis is 0 degrees, X axis - 90 and so on
    //              X /\ 90 deg   
    //                |
    //                |
    // 0 deg Z <------ ------> 180 deg
    //                |
    //                | 
    //                \/ 270 deg
    public float headTiltMin, headTiltMax, lookAngleBegin, lookAngleEnd, rotateSubject;

    private bool CheckIfBetweenTwoAngles(float rbeg, float rend, float ang)
    {
        rbeg = rbeg < 0 ? 360 + rbeg : rbeg;
        rend = rend < 0 ? 360 + rend : rend;
        ang = ang < 0 ? 360 + ang : ang;
        return rbeg < rend ? rbeg <= ang && ang <= rend : rbeg <= ang || ang <= rend;
    }

    private void DoTeleportPlayer(Collider other)
    {
        if (other.gameObject == playerControler.gameObject)
        {

            Vector2 lookang = playerControler.GetLookingAngle();
            if (CheckIfBetweenTwoAngles(headTiltMin, headTiltMax, -lookang.x) && CheckIfBetweenTwoAngles(lookAngleBegin, lookAngleEnd, lookang.y))
            {
                RaycastHit hit;
                Physics.Raycast(playerControler.transform.position, playerControler.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, ~(1 << 2));
                playerControler.transform.rotation *= Quaternion.Euler(0, rotateSubject, 0);
                playerControler.transform.position = teleportLocation.transform.position + new Vector3(0, hit.distance, 0);
            }

        }
    }

    void OnTriggerEnter(Collider o)
    {
        DoTeleportPlayer(o);
    }

    void OnTriggerStay(Collider o)
    {
        DoTeleportPlayer(o);
    }

}
