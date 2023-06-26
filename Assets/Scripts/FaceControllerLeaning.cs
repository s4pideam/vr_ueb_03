using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceControllerLeaning : PlayerController
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 20.0f;



 
    void Start()
    {
        
        var collider = gameObject.GetComponent<CapsuleCollider>();
        controller = gameObject.AddComponent<CharacterController>();
        controller.height = collider.height;
        controller.radius = collider.radius;
        controller.center = collider.center;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3  move = Vector3.one * 0.0001f; ;

        if (faceRect != null)
        {

            if (faceRect.area() > 0)
            {
                Vector2 rect_center = new Vector2(512, 360);
                Vector2 face_center = new Vector2(faceRect.x + faceRect.width / 2, faceRect.y + faceRect.height / 2);
                Vector2 center_dir = (rect_center - face_center).normalized;

                float y = (faceRect.height/720f)-0.3f;
                y *= 3.0f;
                move = new Vector3(center_dir.x,y, 0.0001f);
            }
        }

        controller.Move(move * (Time.deltaTime * playerSpeed));
    }

    private void FixedUpdate()
    {
        if (controller.collisionFlags != CollisionFlags.None)
        {
            //    SceneManager.LoadScene("Exercise 03",LoadSceneMode.Single);
        }
    }
}