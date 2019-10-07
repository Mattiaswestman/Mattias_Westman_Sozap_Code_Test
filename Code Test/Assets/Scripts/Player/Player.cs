using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Invincibility myInvincibility;

    [SerializeField] private KeyCode UpKey = default;
    [SerializeField] private KeyCode DownKey = default;
    [SerializeField] private KeyCode RightKey = default;
    [SerializeField] private KeyCode LeftKey = default;
    
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float turnSpeed = 0f;

    [HideInInspector] public bool isControllable = true;
    [HideInInspector] public bool isAlive = true;
    private bool canMove = false;

    private Vector2 direction = Vector2.right;
    private Vector2 startPosition = Vector2.zero;
    
    private const float RIGHT_MODIFIER = 1f;
    private const float LEFT_MODIFIER = -1f;


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        if(myRigidbody == null)
        {
            Debug.LogError("InputManager: No Rigidbody2D component found on " + gameObject.name);
            enabled = false;
            return;
        }

        myInvincibility = GetComponent<Invincibility>();
        if(myInvincibility == null)
        {
            Debug.LogError("InputManager: No Invincibility component found on " + gameObject.name);
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

            if(canMove)
            {
                Move();
            }
        }
    }

    private void ProcessInput()
    {
        Debug.Log("Test");

        if(Input.GetKey(RightKey))
        {
            Turn(RIGHT_MODIFIER);
        }
        if(Input.GetKey(LeftKey))
        {
            Turn(LEFT_MODIFIER);
        }
        if(Input.GetKeyDown(UpKey))
        {
            
        }
        if(Input.GetKeyDown(DownKey))
        {
            myInvincibility.ActivateInvincibility();
        }
    }

    private void Turn(float directionValue)
    {

    }
    
    private void Move()
    {
        myRigidbody.MovePosition(myRigidbody.position + direction * moveSpeed * Time.deltaTime);
    }

    private void ResetPlayer()
    {
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Level Boundary")
        {
            //isAlive = false;


        }
        else if(collision.tag == "Player Trail" && !myInvincibility.GetIsActive())
        {
            //isAlive = false;


        }
    }
}
