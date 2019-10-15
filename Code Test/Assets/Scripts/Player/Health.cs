using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Invincibility myInvincibility = null;

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }


    private void Awake()
    {
        myInvincibility = GetComponent<Invincibility>();
        if (myInvincibility == null)
        {
            Debug.LogError($"Health: No Invincibility component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAlive = false;

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LevelBoundary")
        {
            isAlive = false;

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Collides with this trail if invincibility is off.
        if (collision.tag == "PlayerTrail" && !myInvincibility.IsActive)
        {
            isAlive = false;

            gameObject.SetActive(false);
        }
    }
}
