using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectileSceneParent = null;
    [SerializeField] private Transform projectileOrigin = null;
    public SpriteRenderer iconRenderer = null;

    [Space(20)]
    [SerializeField] private float projectileSpeed = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isOnCooldown = false;
    public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }


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
        GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, Quaternion.identity) as GameObject;
        projectile.transform.SetParent(projectileSceneParent);
        projectile.GetComponent<Projectile>().owner = gameObject;

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);

        StartCoroutine("CooldownRoutine");
    }
    
    IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        iconRenderer.enabled = false;

        yield return new WaitForSeconds(cooldown);

        iconRenderer.enabled = true;
        isOnCooldown = false;
    }
}
