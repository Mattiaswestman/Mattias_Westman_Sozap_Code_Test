﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mySpaceshipTransform = null;

    [Header("Keys")]
    [SerializeField] private KeyCode UpKey = default;
    [SerializeField] private KeyCode DownKey = default;
    [SerializeField] private KeyCode RightKey = default;
    [SerializeField] private KeyCode LeftKey = default;
    
    [Space(20)]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float turnSpeed = 0f;

    private Rigidbody2D myRigidbody = null;
    private Health myHealth = null;
    private Weapon myWeapon = null;
    private Invincibility myInvincibility = null;

    private Vector2 moveDirection = Vector2.right;
    public Vector2 MoveDirection { set { moveDirection = value; } }

    private bool isControllable = false;
    public bool IsControllable { set { isControllable = value; } }
    private bool canMove = false;
    public bool CanMove { set { canMove = value; } }

    private const float TURN_RIGHT_MODIFIER = -1f;
    private const float TURN_LEFT_MODIFIER = 1f;


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        if (myRigidbody == null)
        {
            Debug.LogError($"InputManager: No Rigidbody2D component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myHealth = GetComponent<Health>();
        if (myHealth == null)
        {
            Debug.LogError($"InputManager: No Health component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myWeapon = GetComponent<Weapon>();
        if (myWeapon == null)
        {
            Debug.LogError($"InputManager: No Weapon component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myInvincibility = GetComponent<Invincibility>();
        if (myInvincibility == null)
        {
            Debug.LogError($"InputManager: No Invincibility component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }
    
    private void Update()
    {
        if (canMove && myHealth.IsAlive)
        {
            ProcessInput();
        }
    }

    private void FixedUpdate()
    {
        if (isControllable && myHealth.IsAlive)
        {
            ProcessFixedInput();

            if (canMove)
            {
                Move();
            }
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(UpKey) && !myWeapon.IsOnCooldown)
        {
            myWeapon.Shoot(moveDirection);
        }
        if (Input.GetKeyDown(DownKey) && !myInvincibility.IsOnCooldown)
        {
            myInvincibility.ActivateInvincibility();
        }
    }

    private void ProcessFixedInput()
    {
        if (Input.GetKey(RightKey))
        {
            Turn(TURN_RIGHT_MODIFIER);
        }
        if (Input.GetKey(LeftKey))
        {
            Turn(TURN_LEFT_MODIFIER);
        }
    }

    private void Turn(float rotationModifier)
    {
        mySpaceshipTransform.Rotate(Vector3.forward, turnSpeed * rotationModifier * Time.fixedDeltaTime);
        moveDirection = mySpaceshipTransform.right;
    }
    
    private void Move()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
