using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private OpenCVForUnity.CoreModule.Rect faceRect ;
    // Start is called before the first frame update
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 20.0f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;


    public void updateFaceRect(OpenCVForUnity.CoreModule.Rect faceRect)
    {
        this.faceRect = faceRect;
    }

    void Start()
    {
        faceRect = new OpenCVForUnity.CoreModule.Rect();
        var collider = gameObject.GetComponent<CapsuleCollider>();
        controller = gameObject.AddComponent<CharacterController>();
        controller.height = collider.height;
        controller.radius = collider.radius;
        controller.center = collider.center;
        


    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        controller.Move(move * (Time.deltaTime * playerSpeed));

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
       // }

       // playerVelocity.y += gravityValue * Time.deltaTime;
       // controller.Move(playerVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (controller.collisionFlags != CollisionFlags.None)
        {
            SceneManager.LoadScene("Exercise 03",LoadSceneMode.Single);
        }
    }
}
