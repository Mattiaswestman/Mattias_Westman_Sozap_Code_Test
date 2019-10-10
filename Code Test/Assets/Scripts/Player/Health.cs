using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Invincibility myInvincibility = null;

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }


    private void Awake()
    {
        myInvincibility = GetComponent<Invincibility>();
        if(myInvincibility == null)
        {
            Debug.LogError($"Health: No Invincibility component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LevelBoundary")
        {
            isAlive = false;

            gameObject.SetActive(false);
        }
        if(collision.tag == "PlayerTrail" && !myInvincibility.IsActive)
        {
            isAlive = false;

            gameObject.SetActive(false);
        }
    }
}
