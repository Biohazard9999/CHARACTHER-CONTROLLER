using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    public float horizontalMove;
    public float verticalMove;

    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    public float gravity;
    public float fallvelocity;
    public float jumForce;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isonSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput,1);


        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position+movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);

        //Debug.Log(player.velocity.magnitude);

    }
    //Funcion para las habilidades de nustro jugador.
    public void PlayerSkills()
    {

        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallvelocity = jumForce;
            movePlayer.y = fallvelocity;
        }
      
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

    }

    void SetGravity()
    {
        
        if (player.isGrounded)
        {
            fallvelocity = -gravity * Time.deltaTime;
            movePlayer.y =fallvelocity;
        }
        else
        {

            fallvelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallvelocity;
        }

        slidedown();
    }



    public void slidedown()
    {
        isonSlope = Vector3.Angle(Vector3.up,hitNormal)>= player.slopeLimit;

        if (isonSlope)
        {
            movePlayer.x += ((1f-hitNormal.y)*hitNormal.x)* slideVelocity;
            movePlayer.z += ((1f-hitNormal.y)*hitNormal.z) * slideVelocity;

            movePlayer.y += slopeForceDown;

        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;


    }


}
