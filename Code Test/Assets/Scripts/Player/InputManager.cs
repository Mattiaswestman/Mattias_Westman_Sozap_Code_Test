using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Rigidbody2D myRigidbody = null;
    private Weapon myWeapon = null;
    private Invincibility myInvincibility = null;

    [SerializeField] private GameObject mySpaceship = null;
    [SerializeField] private KeyCode UpKey = default;
    [SerializeField] private KeyCode DownKey = default;
    [SerializeField] private KeyCode RightKey = default;
    [SerializeField] private KeyCode LeftKey = default;
    
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float turnSpeed = 0f;

    private bool isControllable = true;
    private bool isAlive = true;
    private bool canMove = true;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 startPosition = Vector2.zero;
    
    private const float RIGHT_TURN_MODIFIER = -1f;
    private const float LEFT_TURN_MODIFIER = 1f;


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        if(myRigidbody == null)
        {
            Debug.LogError($"InputManager: No Rigidbody2D component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myInvincibility = GetComponent<Invincibility>();
        if(myInvincibility == null)
        {
            Debug.LogError($"InputManager: No Invincibility component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myWeapon = GetComponent<Weapon>();
        if(myWeapon == null)
        {
            Debug.LogError($"InputManager: No Weapon component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if(isControllable && isAlive)
        {
            ProcessInput();
        }
    }

    private void FixedUpdate()
    {
        if(isControllable && isAlive)
        {
            ProcessFixedInput();

            if(canMove)
            {
                Move();
            }
        }
    }

    private void ProcessInput()
    {
        if(Input.GetKeyDown(UpKey) && !myWeapon.GetIsOnCooldown())
        {
            myWeapon.Shoot(moveDirection);
        }
        if(Input.GetKeyDown(DownKey) && !myInvincibility.GetIsOnCooldown())
        {
            myInvincibility.ActivateInvincibility();
        }
    }

    private void ProcessFixedInput()
    {
        if(Input.GetKey(RightKey))
        {
            Turn(RIGHT_TURN_MODIFIER);
        }
        if(Input.GetKey(LeftKey))
        {
            Turn(LEFT_TURN_MODIFIER);
        }
    }

    private void Turn(float rotateValue)
    {
        mySpaceship.transform.Rotate(Vector3.forward, turnSpeed * rotateValue * Time.fixedDeltaTime);
        moveDirection = mySpaceship.transform.right;
    }
    
    private void Move()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void ResetPlayer()
    {
        transform.position = startPosition;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LevelBoundary")
        {
            isAlive = false;


        }
        if(collision.tag == "PlayerTrail" && !myInvincibility.GetIsActive())
        {
            isAlive = false;


        }
    }
}
