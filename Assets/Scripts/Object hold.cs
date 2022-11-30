
using UnityEngine;




public class ObjectHold : MonoBehaviour

{

    public GameObject Object;

    public Transform PlayerTransform;

    public float range = 3f;

    public float Go = 100f;

    public Camera Camera;







    void Start()

    {



    }

#pragma warning disable IDE0051 // Remove unused private members
    private void StartPickUp()
#pragma warning restore IDE0051 // Remove unused private members

    {


        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, range))

        {

            Debug.Log(hit.transform.name);




            Target target = hit.transform.GetComponent<Target>();

            if (target != null)

            {

                PickUp();

            }

        }

    }

    private void PickUp()

    {
        GameObject @object = Object;
        @object.transform.SetParent(PlayerTransform);
    }
}
