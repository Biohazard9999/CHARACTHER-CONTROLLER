using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController player;
    Vector3 groudPosition;
    Vector3 lastGroudPosition;
    string groudName;
    string lastGroundName;

    Quaternion actualRot;
    Quaternion lastRot;



    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGrounded)
        {

            RaycastHit hit;
            if (Physics.SphereCast(transform.position,player.height/4.2f,-transform.up,out hit))
            {
                GameObject groundedIn = hit.collider.gameObject;
                groudName = groundedIn.name;
                groudPosition = groundedIn.transform.position;
                actualRot = groundedIn.transform.rotation;

                if (groudPosition != lastGroudPosition && groudName == lastGroundName)
                {
                

                    this.transform.position +=groudPosition-lastGroudPosition;

                    player.enabled = false;
                    player.transform.position = this.transform.position;
                    player.enabled = true;

                }
                
                if (actualRot!=lastRot && groudName==lastGroundName)
                {
                    var newRot = this.transform.rotation * (actualRot.eulerAngles - lastRot.eulerAngles);
                    this.transform.RotateAround(groundedIn.transform.position,Vector3.up, newRot.y);

                }

                lastGroundName = groudName;
                lastGroudPosition = groudPosition;
                lastRot = actualRot;
            }


        }

        else if (!player.isGrounded)
        {
         
            lastGroundName = null;
            lastGroudPosition = Vector3.zero;
            lastRot = Quaternion.Euler(0,0,0);

        }
       

    }

    private void OnDrawGizmos()
    {

        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position, player.height / 4.2f);

    }

}
