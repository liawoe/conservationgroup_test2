using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinCollision : MonoBehaviour
{

    public static Dictionary<string, Dictionary<string, float>> connectionmap = new Dictionary<string, Dictionary<string, float>>()
        {
            {"C", new Dictionary<string, float>(){
                {"H", 2},
                {"I", 5},
                {"S", 10}
            }},
            {"I", new Dictionary<string, float>(){
                {"C", 5},
                {"H", 3},
                {"S", 12}
            }},
            {"H", new Dictionary<string, float>(){
                {"C", 2},
                {"I", 3},
                {"S", 1}
            }},
            {"S", new Dictionary<string, float>(){
                {"H", 1},
                {"I", 12},
                {"C", 10}
            }},
        };

    public AudioClip collision;
    public AudioClip breaknoise;
    private AudioSource _a_src;

    void Start()
    {
        collision = Resources.Load<AudioClip>("mixkit-metal-bowl-hit-1842");
        breaknoise = Resources.Load<AudioClip>("mixkit-game-ball-tap-2073");
        _a_src = gameObject.AddComponent<AudioSource>() as AudioSource;
        _a_src.playOnAwake = false;
    }

    void OnCollisionEnter(Collision col)
    {
        float velocity = col.relativeVelocity.magnitude;

        if (col.gameObject.tag == "JoinCollider" && col.rigidbody)
        {
            bool jointexists = false;
            foreach (FixedJoint j in gameObject.GetComponents<FixedJoint>())
            {
                if (j.connectedBody == col.rigidbody)
                {
                    jointexists = true;
                    break;
                }
            }

            foreach (FixedJoint j in col.gameObject.GetComponents<FixedJoint>())
            {
                if (j.connectedBody == gameObject.GetComponent<Rigidbody>())
                {
                    jointexists = true;
                    break;
                }
            }

            if (!jointexists && VelocityMatch(velocity, col.gameObject.GetComponent<Renderer>().material.name[0].ToString()))
            {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>() as FixedJoint;
                joint.connectedBody = col.rigidbody;
                joint.breakForce = 1200;
                _a_src.PlayOneShot(collision);
            }
        }
    }

    void OnJointBreak(float brkfrc)
    {
        Debug.Log(brkfrc);
        _a_src.PlayOneShot(breaknoise);
    }

    bool VelocityMatch(float colpoint, string material)
    {
        string selfname = gameObject.GetComponent<Renderer>().material.name[0].ToString();
        float neededpoints = connectionmap[selfname][material];
        Debug.Log(colpoint + " " + neededpoints);
        if (material == selfname ||
           neededpoints <= colpoint) return true;
        return false;
    }
}
