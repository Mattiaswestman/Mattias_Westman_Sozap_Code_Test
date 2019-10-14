using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer iconRenderer = null;
    [SerializeField] private SpriteRenderer[] spaceshipRenderers = null;

    [Space(20)]
    [SerializeField] private float duration = 0f;
    [SerializeField] private float cooldown = 0f;

    private bool isActive = false;
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    private bool isOnCooldown = false;
    public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }

    private Coroutine invincibilityRoutine = null;


    public void ActivateInvincibility()
    {
        invincibilityRoutine = StartCoroutine(InvincibilityRoutine(duration));
    }
    
    public void SetAlpha(SpriteRenderer spriteRenderer, float newAlpha)
    {
        Color temp = spriteRenderer.color;
        temp.a = newAlpha;
        spriteRenderer.color = temp;
    }

    public void SetShipAlpha(float newAlpha)
    {
        for(int i = 0; i < spaceshipRenderers.Length; i++)
        {
            Color temp = spaceshipRenderers[i].color;
            temp.a = newAlpha;
            spaceshipRenderers[i].color = temp;
        }
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        var timer = 0f;

        isActive = true;
        iconRenderer.enabled = false;

        while(timer < duration)
        {
            float alphaThisFrame = (Mathf.Sin(Time.time * 10f) + 1f) * 0.5f;

            for(int i = 0; i < spaceshipRenderers.Length; i++)
            {
                SetShipAlpha(alphaThisFrame);
            }

            timer += Time.deltaTime;
            yield return null;
        }
        
        for(int i = 0; i < spaceshipRenderers.Length; i++)
        {
            SetShipAlpha(1f);
        }

        isActive = false;
        StartCoroutine("CooldownRoutine");
    }

    IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);
        
        iconRenderer.enabled = true;
        isOnCooldown = false;
    }
}
