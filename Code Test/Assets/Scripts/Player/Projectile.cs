using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject owner;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the projectile collides with another player it updates the round score of the player firing.
        if (collision.tag == "Player" && collision.gameObject != owner)
        {
            owner.GetComponent<RoundScore>().UpdateScore();

            enabled = false;
            Destroy(gameObject);
        }
        if (collision.tag == "LevelBoundary" || collision.tag == "PlayerTrail")
        {
            enabled = false;
            Destroy(gameObject);
        }
    }
}
