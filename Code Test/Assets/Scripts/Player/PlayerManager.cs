﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (myInputManager == null)
        {
            Debug.LogError($"PlayerManager: No InputManager component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myHealth = GetComponent<Health>();
        if (myHealth == null)
        {
            Debug.LogError($"PlayerManager: No Health component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myWeapon = GetComponent<Weapon>();
        if (myWeapon == null)
        {
            Debug.LogError($"PlayerManager: No Weapon component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myInvincibility = GetComponent<Invincibility>();
        if (myInvincibility == null)
        {
            Debug.LogError($"PlayerManager: No Invincibility component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myTrail = GetComponent<Trail>();
        if (myTrail == null)
        {
            Debug.LogError($"PlayerManager: No Trail component found on {gameObject.name}.");
            enabled = false;
            return;
        }

        myRoundScore = GetComponent<RoundScore>();
        if (myRoundScore == null)
        {
            Debug.LogError($"PlayerManager: No RoundScore component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        SetStartPosition();
        SetStartRotation();
    }

    // Called from GameManager to give players turn control, if true. Will also reveal round score above players.
    //
    public void SetPlayerIsControllable(bool value)
    {
        myInputManager.IsControllable = value;
        myRoundScore.SetScoreVisibility(value);
    }

    // Called from GameManager to start player movement, trail generation, and give players control over weapon and invincibility, if true.
    //
    public void SetPlayerCanMove(bool value)
    {
        myInputManager.CanMove = value;

        myWeapon.StopAllCoroutines();
        myWeapon.SetIconVisibility(value);

        myInvincibility.StopAllCoroutines();
        myInvincibility.SetIconVisibility(value);

        myTrail.StopAllCoroutines();
        myTrail.IsPaused = !value;
    }

    // Resets the player to its default state. Is called from GameManager when a player reset is needed, i.e. round reset, game reset.
    //
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
        myWeapon.SetIconVisibility(false);

        myInvincibility.IsActive = false;
        myInvincibility.IsOnCooldown = false;
        myInvincibility.SetShipAlpha(1f);
        myInvincibility.SetIconVisibility(false);

        myTrail.IsDrawing = false;
        myTrail.IsPaused = true;

        myRoundScore.ResetScore();
        myRoundScore.SetScoreVisibility(false);
    }

    private void SetStartPosition()
    {
        startPosition = Camera.main.ScreenToWorldPoint(startPositionRectTransform.transform.position);
        transform.position = startPosition;
    }

    private void SetStartRotation()
    {
        startRotation = Quaternion.Euler(Vector3.right);
        mySpaceshipTransform.rotation = startRotation;
    }
}
