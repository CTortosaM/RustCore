﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public string mouseXInputName, mouseYInputName;
    public float mouseSensitivity;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

    private void Awake()
    {
        PlayerController.onPlayerDash += startDashAnimation;
        LockCursor();
        Cursor.visible = false;
        xAxisClamp = 0.0f;
        if(!animator) animator = GetComponent<Animator>();
        
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    private void startDashAnimation()
    {
        if (!animator) animator = FindObjectOfType<PlayerLook>().GetComponent<Animator>();
        if(animator) animator.Play("dash_fov_increase");
    }
}
