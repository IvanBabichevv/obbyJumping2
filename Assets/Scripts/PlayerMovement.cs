using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}
    
    [SerializeField] private CharacterController conroller;
    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravity = -9.8f;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float IncreaseCooldown = 0.1f;

    public float GetJumpPower() => jumpHeight;
    
    
    Vector3 velocity;
    bool isGrounded;
    
    float turnSmoothVelocity;
    private float nextIncreaseCooldown = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        Vector3 move = new Vector3(x, 0, z);

        if (move.magnitude > 1f)
            move.Normalize();

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            conroller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            print("Jump with height: " + jumpHeight.ToString("F2"));
        }

        if (Input.GetMouseButton(0) && Time.time >= nextIncreaseCooldown)
        {
            
        }

        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        } 
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        conroller.Move(velocity * Time.deltaTime);
        
    }

    public void IncreaseJumpPower()
    {
        jumpHeight += 0.005f;
        jumpHeight = Mathf.Clamp(jumpHeight, 0.001f, 50f);
        nextIncreaseCooldown = Time.time + IncreaseCooldown;
        PointsManager.Instance.ScoreChangedInvoke();
        print("Jump power increased: "  + jumpHeight.ToString("F2"));
    }

   
}

