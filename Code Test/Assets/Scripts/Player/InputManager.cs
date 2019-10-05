﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField] private KeyCode UpKey = default;
    [SerializeField] private KeyCode DownKey = default;
    [SerializeField] private KeyCode RightKey = default;
    [SerializeField] private KeyCode LeftKey = default;
    
    [SerializeField] private float speed = 0f;
    [SerializeField] private float heartCooldown = 0f;
    [SerializeField] private float ammoCooldown = 0f;

    private const float RIGHT = 1f;
    private const float LEFT = -1f;


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        if(myRigidbody == null)
        {
            Debug.LogError("InputManager: No Rigidbody2D component found on " + gameObject.name);
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if(Input.GetKey(RightKey))
        {
            Turn(RIGHT);
        }
        if(Input.GetKey(LeftKey))
        {
            Turn(LEFT);
        }
        if(Input.GetKeyDown(UpKey))
        {
            ShootProjectile();
        }
        if(Input.GetKeyDown(DownKey))
        {
            UseInvincibility();
        }
    }

    private void Turn(float directionValue)
    {

    }

    private void ShootProjectile()
    {

    }

    private void UseInvincibility()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
