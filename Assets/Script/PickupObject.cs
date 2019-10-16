using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject objecToPickUp;
    public GameObject Pickedobject;
    public Transform interactionZone;


 
    void Update()
    {

        if (objecToPickUp != null && objecToPickUp.GetComponent<PickableOcject>().isPickable == true && Pickedobject == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Pickedobject = objecToPickUp;
                Pickedobject.GetComponent<PickableOcject>().isPickable = false;
                Pickedobject.transform.SetParent(interactionZone);
                Pickedobject.transform.position = interactionZone.position;
                Pickedobject.GetComponent<Rigidbody>().useGravity = false;
                Pickedobject.GetComponent<Rigidbody>().isKinematic = true;

            }

        }
        else if (Pickedobject != null)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                Pickedobject.GetComponent<PickableOcject>().isPickable = true;
                Pickedobject.transform.SetParent(null);
                Pickedobject.GetComponent<Rigidbody>().useGravity = true;
                Pickedobject.GetComponent<Rigidbody>().isKinematic = false;
                Pickedobject = null;


            }


        }


        


    }
}
