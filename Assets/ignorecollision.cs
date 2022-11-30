using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorecollision : MonoBehaviour
{
    public GameObject toignore;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(toignore.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
