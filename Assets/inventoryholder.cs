using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class inventoryholder : MonoBehaviour
{

    private ConstraintSource constraintSource;
    private float entrytime;
    // Start is called before the first frame update
    void Start()
    {
        entrytime = Time.time;
        constraintSource.sourceTransform = transform;
        constraintSource.weight = 1;
    }

    public void LockObject()
    {

        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale * 0.5f, transform.rotation, 1 << 3);

        if (colliders[0].gameObject.tag == "InventoryObject" && (Time.time - entrytime) > 1)
        {
            Debug.Log("collision entered");
            colliders[0].gameObject.GetComponent<Rigidbody>().isKinematic = true;
            entrytime = Time.time;
            ParentConstraint prnt = colliders[0].gameObject.AddComponent<ParentConstraint>();
            prnt.AddSource(constraintSource);
            prnt.constraintActive = true;
            prnt.locked = false;
            prnt.translationOffsets = new Vector3[] { transform.InverseTransformPoint(colliders[0].gameObject.transform.position) };
            prnt.rotationOffsets = new Vector3[] { transform.InverseTransformDirection(colliders[0].gameObject.transform.rotation.eulerAngles) };
            //prnt.translationAxis = Axis.X | Axis.Y | Axis.Z;
            //collision.gameObject.transform.SetParent(transform, true);
        }
    }

    public void UnlockObject()
    {

        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale * 0.5f, transform.rotation, 1 << 3);

        if (colliders[0].gameObject.tag == "InventoryObject" && (Time.time - entrytime) > 1)
        {
            Debug.Log("collision exited");
            entrytime = Time.time;
            //collision.gameObject.transform.SetParent(null, true);
            Destroy(colliders[0].gameObject.GetComponent<ParentConstraint>());
            colliders[0].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
