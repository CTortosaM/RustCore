﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealtAndShield))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isDead;
    [SerializeField] private int health = 100;
    [SerializeField] private int shield = 100;
    public Camera[] cameras;
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private CharacterController charController;
    private HealtAndShield healtAndShield;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode dashKey;
    [SerializeField] private float dashSpeedMultiplier;
    [SerializeField] private int totalJumps;

    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashDuration;

    //Variables de control de cooldown
    private bool isJumping;
    private bool isDashing;
    public bool canDash;
    private float nextPossibleDashTime;

    //Propiedades
    public float DashCoolDown { get => dashCoolDown; set => dashCoolDown = value; }
    public float DashDuration { get => dashDuration; set => dashDuration = value; }
    public int Health { get => health; set => health = value; }
    public int Shield { get => shield; set => shield = value; }

    private void Awake()
    {
        //LevelBuilder.startingGeneration += TurnOffCameras;
        isDead = false;
        charController = GetComponent<CharacterController>();
        healtAndShield = GetComponent<HealtAndShield>();
        canDash = true;
        
        //LevelBuilder.onLevelFinished += TurnOnCameras;
        
    }

    private void Update()
    {
        if(!HealtAndShield.IsDead) PlayerMovement();
        if (!isJumping && !HealtAndShield.IsDead) totalJumps = 2;
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);

        JumpInput();
        DashInput();
    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && totalJumps > 0)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }


    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        totalJumps -= 1;
        float timeInAir = 0.0f;
        do
        {
            if (totalJumps == 0)
            {
                //charController.Move(charController.);
            }
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }

    private void DashInput()
    {
        if (Input.GetButtonDown("Dash") && canDash && Time.time> nextPossibleDashTime  || Input.GetAxis("Dash") > 0 && canDash && Time.time > nextPossibleDashTime)
        {
            nextPossibleDashTime = Time.time + DashCoolDown;
            canDash = false;
            isDashing = true;
            StartCoroutine(dashEvent());
        }
    }

    private IEnumerator dashEvent()
    {

        float OGSpeed = movementSpeed;
        movementSpeed *= dashSpeedMultiplier;
        yield return new WaitForSeconds(DashDuration);
        movementSpeed = 16.5f;
        charController.Move(new Vector3(0, 0, 0));
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }


    private void TurnOnCameras()
    {
        foreach(Camera cam in cameras)
        {
            if(cam) cam.gameObject.SetActive(true);
        }
    }

    private void TurnOffCameras()
    {
        foreach (Camera cam in cameras)
        {
            if (cam) cam.gameObject.SetActive(false);
        }
    }

}


