using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class hapticfeedbackhands : MonoBehaviour
{

    private ActionBasedController xr;
    // Start is called before the first frame update
    void Start()
    {
        xr = GetComponent(typeof(ActionBasedController)) as ActionBasedController;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) {
            Debug.Log("handcol");
            xr.SendHapticImpulse(1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
