using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Rigidbody platfomRB;
    public Transform[] platformPositions;
    public float platformspeed;

    private int actualPosition = 0;
    private int nexPosition = 1;

    public bool moveToTheNext = true;
    public float waitTime;
  
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (moveToTheNext)
        {
            StopCoroutine(waiForMove(0));
            platfomRB.MovePosition(Vector3.MoveTowards(platfomRB.position, platformPositions[nexPosition].position, platformspeed * Time.deltaTime));

        }

       

        if (Vector3.Distance(platfomRB.position, platformPositions[nexPosition].position)<=0)
        {
            StartCoroutine(waiForMove(waitTime));
            actualPosition = nexPosition;
            nexPosition++;

            if (nexPosition>platformPositions.Length-1)
            {
                nexPosition = 0;
            }

        }


    }


    IEnumerator waiForMove(float time)
    {

        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;

    }

}
