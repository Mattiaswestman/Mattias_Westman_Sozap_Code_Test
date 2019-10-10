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
            owner.GetComponent<RoundScore>().UpdateRoundScore();

            enabled = false;
            Destroy(gameObject);
        }
        if(collision.tag == "LevelBoundary" || collision.tag == "PlayerTrail")
        {
            enabled = false;
            Destroy(gameObject);
        }
    }
}
