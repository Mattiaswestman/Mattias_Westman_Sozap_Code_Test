using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [SerializeField] GameObject invincibilityObject = null;

    [SerializeField] private float duration = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isActive = false;
    private bool isOnCooldown = false;

    public void ActivateInvincibility()
    {
        if(!isOnCooldown)
        {
            StartCoroutine("InvincibilityRoutine");
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    IEnumerator InvincibilityRoutine()
    {
        invincibilityObject.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("Invincible!");
        isActive = true;

        yield return new WaitForSeconds(duration);

        isActive = false;

        StartCoroutine("CooldownRoutine");
    }

    IEnumerator CooldownRoutine()
    {
        invincibilityObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Debug.Log("On cooldown..");
        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);

        invincibilityObject.GetComponent<SpriteRenderer>().color = Color.white;
        isOnCooldown = false;
    }
}
