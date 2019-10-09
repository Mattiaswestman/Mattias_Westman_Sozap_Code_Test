using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [SerializeField] SpriteRenderer iconRenderer = null;

    [SerializeField] private float duration = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isActive = false;
    private bool isOnCooldown = false;


    public void ActivateInvincibility()
    {
        StartCoroutine("InvincibilityRoutine");
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public bool GetIsOnCooldown()
    {
        return isOnCooldown;
    }

    IEnumerator InvincibilityRoutine()
    {
        isActive = true;
        iconRenderer.color = Color.red;

        yield return new WaitForSeconds(duration);

        isActive = false;

        StartCoroutine("CooldownRoutine");
    }

    IEnumerator CooldownRoutine()
    {
        iconRenderer.color = Color.blue;
        
        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);

        iconRenderer.color = Color.white;
        isOnCooldown = false;
    }
}
