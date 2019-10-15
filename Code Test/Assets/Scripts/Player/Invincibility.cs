using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer iconRenderer = null;
    [SerializeField] private SpriteRenderer[] spaceshipRenderers = null;

    [Space(20)]
    [SerializeField] private float duration = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isActive = false;
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    private bool isOnCooldown = false;
    public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }

    private Coroutine invincibilityRoutine = null;
    private Coroutine cooldownRoutine = null;

    private const float SIN_CURVE_MODIFIER = 10f;


    // Starts an invincibility coroutine when called from the InputManager.
    //
    public void ActivateInvincibility()
    {
        invincibilityRoutine = StartCoroutine(InvincibilityRoutine(duration));
    }
    
    // Changes the alpha values of the different spaceship sprites.
    //
    public void SetShipAlpha(float newAlpha)
    {
        for (int i = 0; i < spaceshipRenderers.Length; i++)
        {
            Color temp = spaceshipRenderers[i].color;
            temp.a = newAlpha;
            spaceshipRenderers[i].color = temp;
        }
    }

    public void SetIconVisibility(bool value)
    {
        iconRenderer.enabled = value;
    }

    // The coroutine sets an invincibility bool variable active over its duration, which is checked against when colliding with trails.
    //
    IEnumerator InvincibilityRoutine(float duration)
    {
        var timer = 0f;

        isActive = true;
        iconRenderer.enabled = false;

        // The spaceship pulses while invincibility is active, which is solved by changing the alpha value of its sprites.
        while (timer < duration)
        {
            // The alpha value follows a sin curve between values 0 and 1.
            float alphaThisFrame = (Mathf.Sin(Time.time * SIN_CURVE_MODIFIER) + 1f) * 0.5f;

            SetShipAlpha(alphaThisFrame);

            timer += Time.deltaTime;
            yield return null;
        }
        
        // Makes sure that the alpha is returned to 1 after invincibility is over.
        SetShipAlpha(1f);

        isActive = false;
        cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    // Invincibility is followed by a cooldown coroutine, that hinders it from being activated again over its duration.
    //
    IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);
        
        iconRenderer.enabled = true;
        isOnCooldown = false;
    }
}
