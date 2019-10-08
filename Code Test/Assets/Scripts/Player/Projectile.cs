using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject owner;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.gameObject != owner)
        {


            enabled = false;
            Destroy(gameObject);
        }
        if(collision.tag == "LevelBoundary")
        {
            enabled = false;
            Destroy(gameObject);
        }
    }
}
