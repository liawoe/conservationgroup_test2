using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{

    public GameObject objectToSpawn;

    void Start()
    {

    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "JoinCollider" && col.rigidbody)
        {
            Instantiate(objectToSpawn, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

}
