using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class FaceController : PlayerController
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 20.0f;

    public Vector3 oldMove;
    private Vector3 velocity = Vector3.zero;

 
    void Start()
    {
        
        var collider = gameObject.GetComponent<CapsuleCollider>();
        controller = gameObject.AddComponent<CharacterController>();
        controller.height = collider.height;
        controller.radius = collider.radius;
        controller.center = collider.center;
        oldMove = Vector3.one * 0.0001f;
    }

    private float remap(float inputmin, float inputmax, float input, float outputmin, float outputmax)
    {
        float normal = Mathf.InverseLerp(inputmin, inputmax, input);
        float value = Mathf.Lerp(outputmin, outputmax, normal);
        return value;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3  move = Vector3.one * 0.0001f;

        if (faceRect != null)
        {

            if (faceRect.area() > 0)
            {
                Vector3 face_center = new Vector3(faceRect.x + faceRect.width / 2, faceRect.y + faceRect.height / 2,0f);
                float horizontal = remap(0, 960, face_center.x, -2, 2);
                float vertical = remap(0, 540, face_center.y, -2, 2);
                move = new Vector3(-horizontal,-vertical, 0.0001f);
            }
        }

   
        controller.Move( move * (Time.deltaTime * playerSpeed));
        oldMove = move;
    }

    private void FixedUpdate()
    {
        if (controller.collisionFlags != CollisionFlags.None)
        {
            //    SceneManager.LoadScene("Exercise 03",LoadSceneMode.Single);
        }
    }
}
