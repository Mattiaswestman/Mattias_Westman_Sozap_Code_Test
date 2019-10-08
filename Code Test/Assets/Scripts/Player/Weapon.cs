using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] SpriteRenderer iconRenderer = null;

    [SerializeField] Transform projectileSceneParent = null;
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] Transform projectileSpawnTransform = null;

    [SerializeField] private float projectileSpeed = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isOnCooldown = false;


    private void Awake()
    {
        if(projectileSceneParent == null)
        {
            Debug.LogError($"Weapon: No reference to Projectile Scene Parent set on {gameObject.name}.");
            enabled = false;
            return;
        }

        if(projectilePrefab == null)
        {
            Debug.LogError($"Weapon: No reference to Projectile Prefab set on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    public void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnTransform.position, Quaternion.identity) as GameObject;
        projectile.transform.SetParent(projectileSceneParent);

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);

        StartCoroutine("CooldownRoutine");
    }

    public bool GetIsOnCooldown()
    {
        return isOnCooldown;
    }

    IEnumerator CooldownRoutine()
    {
        iconRenderer.color = Color.blue;
        Debug.Log("On cooldown..");
        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);

        iconRenderer.color = Color.white;
        isOnCooldown = false;
    }
}
