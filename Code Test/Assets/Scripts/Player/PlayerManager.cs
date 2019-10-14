using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mySpaceshipTransform = null;
    [SerializeField] private RectTransform startPositionRectTransform = null;

    private InputManager myInputManager = null;
    private Health myHealth = null;
    private Weapon myWeapon = null;
    private Invincibility myInvincibility = null;
    private Trail myTrail = null;
    private RoundScore myRoundScore = null;

    private Vector2 startPosition = Vector2.zero;
    private Quaternion startRotation = default;


    private void Awake()
    {
        myInputManager = GetComponent<InputManager>();
        if(myInputManager == null)
        {
            Debug.LogError($"PlayerManager: No InputManager component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myHealth = GetComponent<Health>();
        if(myHealth == null)
        {
            Debug.LogError($"PlayerManager: No Health component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myWeapon = GetComponent<Weapon>();
        if(myWeapon == null)
        {
            Debug.LogError($"PlayerManager: No Weapon component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myInvincibility = GetComponent<Invincibility>();
        if(myInvincibility == null)
        {
            Debug.LogError($"PlayerManager: No Invincibility component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myTrail = GetComponent<Trail>();
        if(myTrail == null)
        {
            Debug.LogError($"PlayerManager: No Trail component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myRoundScore = GetComponent<RoundScore>();
        if(myRoundScore == null)
        {
            Debug.LogError($"PlayerManager: No RoundScore component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        startPosition = Camera.main.ScreenToWorldPoint(startPositionRectTransform.transform.position);
        transform.position = startPosition;

        startRotation = Quaternion.Euler(Vector3.right);
        mySpaceshipTransform.rotation = startRotation;
    }

    public void SetPlayerIsControllable(bool value)
    {
        myInputManager.IsControllable = value;

        myRoundScore.scoreRenderer.enabled = value;
    }

    public void SetPlayerCanMove(bool value)
    {
        myInputManager.CanMove = value;
        myTrail.IsPaused = !value;

        myWeapon.StopAllCoroutines();
        myWeapon.iconRenderer.enabled = value;
        myInvincibility.StopAllCoroutines();
        myInvincibility.iconRenderer.enabled = value;
    }

    public void ResetPlayer()
    {
        gameObject.SetActive(true);

        myInputManager.CanMove = false;
        myInputManager.IsControllable = false;

        transform.position = startPosition;
        mySpaceshipTransform.rotation = startRotation;
        myInputManager.MoveDirection = startRotation.eulerAngles;
        
        myHealth.IsAlive = true;

        myWeapon.IsOnCooldown = false;
        myWeapon.iconRenderer.enabled = false;

        myInvincibility.IsActive = false;
        myInvincibility.IsOnCooldown = false;
        myInvincibility.SetShipAlpha(1f);
        myInvincibility.iconRenderer.enabled = false;

        myTrail.IsDrawing = false;
        myTrail.IsPaused = true;

        myRoundScore.ResetRoundScore();
        myRoundScore.scoreRenderer.enabled = false;
    }
}
