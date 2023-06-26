using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowController
    : PlayerController
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 20.0f;



 
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
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0001f);
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