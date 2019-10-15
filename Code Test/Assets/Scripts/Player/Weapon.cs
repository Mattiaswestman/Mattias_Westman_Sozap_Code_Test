using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer iconRenderer = null;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectileSceneParent = null;
    [SerializeField] private Transform projectileOrigin = null;

    [Space(20)]
    [SerializeField] private float projectileSpeed = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isOnCooldown = false;
    public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }

    private Coroutine cooldownRoutine = null;


    private void Awake()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError($"Weapon: No reference to Projectile Prefab set on {gameObject.name}.");
            enabled = false;
            return;
        }

        if (projectileSceneParent == null)
        {
            Debug.LogError($"Weapon: No reference to Projectile Scene Parent set on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    public void SetIconVisibility(bool value)
    {
        iconRenderer.enabled = value;
    }

    // Spawns and fires a projectile in the given direction when called from the InputManager.
    //
    public void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, Quaternion.identity) as GameObject;
        // All projectiles are grouped in a mutual scene parent.
        projectile.transform.SetParent(projectileSceneParent);
        // Sets this player as the owner of the projectile, to know which score to increase on a hit.
        projectile.GetComponent<Projectile>().owner = gameObject;

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);

        cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    // Shooting is followed by a cooldown coroutine, that hinders the player from shooting again over its duration.
    //
    IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        iconRenderer.enabled = false;

        yield return new WaitForSeconds(cooldown);

        iconRenderer.enabled = true;
        isOnCooldown = false;
    }
}
