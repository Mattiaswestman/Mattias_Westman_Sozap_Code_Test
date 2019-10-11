using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer iconRenderer = null;

    [Space(20)]
    [SerializeField] private float duration = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isActive = false;
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    private bool isOnCooldown = false;
    public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }


    public void ActivateInvincibility()
    {
        StartCoroutine("InvincibilityRoutine");
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
