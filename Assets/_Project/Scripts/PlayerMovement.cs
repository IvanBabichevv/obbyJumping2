using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [SerializeField] private CharacterController conroller;
    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravity = -9.8f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float floorDistance = 1.5f;
    [SerializeField] private float IncreaseCooldown = 0.1f;

    [SerializeField] public float oneClick = 1 / 200f;

    private Animator animator;

    private float stepCoolDown = 0.3f;
    private float nextStepTime = 0f;

    public int GetJumpPower() => (int)(jumpHeight * 200);

    Vector3 velocity;
    bool isGrounded;

    float turnSmoothVelocity;
    private float nextIncreaseCooldown = 0;

    private Vector3 lastPosition;
    private Vector3 startPosition;
    private float moveMinimum = 0.001f;

    float horizontal;
    float vertical;

    // *** Jump logic ***
    private float jumpStartY;
    private bool isJumping;
    private bool stopJumpAllowed;

    [Header("High Jump Settings")] public float characterHeight = 2f; // высота персонажа
    public float highJumpThresholdMultiplier = 2.5f; // высота (2-3 метра)
    public Button stopJumpButton; // кнопка сброса прыжка

    private void OnEnable()
    {
        TouchMovementController.movementController = this;
        TouchMovementController.OnJumpButtonDown.AddListener(Jump);
    }

    private void OnDisable()
    {
        TouchMovementController.movementController = null;
        TouchMovementController.OnJumpButtonDown.RemoveListener(Jump);

        if (stopJumpButton != null)
            stopJumpButton.onClick.RemoveListener(StopJump);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        jumpHeight = YG2.saves.jumpHeight;

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        lastPosition = transform.position;
        startPosition = transform.position;

        PointsManager.Instance.ScoreChangedInvoke();

        // Кнопка изначально скрыта
        if (stopJumpButton != null)
        {
            stopJumpButton.gameObject.SetActive(false);
            stopJumpButton.onClick.AddListener(StopJump);
        }
    }

    void Update()
    {
        isGrounded = conroller.isGrounded;

        if (transform.position.y < -2)
        {
            conroller.enabled = false;
            transform.position = startPosition;
            conroller.enabled = true;
        }

        if (YG2.envir.isDesktop)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        Vector3 move = new Vector3(horizontal, 0, vertical);

        if (move.magnitude > 1f)
            move.Normalize();

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            conroller.Move(moveDir.normalized * (speed * Time.deltaTime));
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, floorDistance, floorMask))
        {
            velocity.y = -10f;
        }

        // приземление
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            if (isJumping)
            {
                isJumping = false;
                stopJumpAllowed = false;
                HideStopButton();
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        conroller.Move(velocity * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = distance > moveMinimum;

        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
            animator.SetBool("isJumping", !isGrounded);
        }

        if (isGrounded && isMoving && Time.time >= nextStepTime)
        {
            SoundManager.instance.PlayFootStep();
            nextStepTime = Time.time + stepCoolDown;
        }

        lastPosition = transform.position;

        // Высокий прыжок = появление кнопки
        if (isJumping)
        {
            float heightNow = transform.position.y - jumpStartY;

            if (!stopJumpAllowed && heightNow >= characterHeight * highJumpThresholdMultiplier)
            {
                if (velocity.y > 0)
                {
                    stopJumpAllowed = true;
                    ShowStopButton();
                }
            }

            if (stopJumpAllowed && Input.GetKeyDown(KeyCode.Space))
            {
                StopJump();
            }
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            jumpStartY = transform.position.y;
            isJumping = true;
            stopJumpAllowed = false;

            HideStopButton(); // кнопка скрыта в начале прыжка
        }
    }

    public void StopJump()
    {
        if (stopJumpAllowed)
        {
            velocity.y = -15f; // убираем ускорение вверх

            stopJumpAllowed = false;
            HideStopButton();
        }
    }

    private void ShowStopButton()
    {
        if (stopJumpButton != null)
            stopJumpButton.gameObject.SetActive(true);
    }

    public void HideStopButton()
    {
        if (stopJumpButton != null)
            stopJumpButton.gameObject.SetActive(false);
    }

    public void SetAxis(Vector3 direction)
    {
        horizontal = direction.x;
        vertical = direction.z;
    }

    public void IncreaseJumpPower()
    {
        jumpHeight += oneClick * PointsManager.Instance.CurrentCoefficient;
        nextIncreaseCooldown = Time.time + IncreaseCooldown;
        PointsManager.Instance.ScoreChangedInvoke();

        YG2.saves.jumpHeight = jumpHeight;
        YG2.SaveProgress();
    }

    public void ForceStop(bool value)
    {
        if (conroller)
            conroller.enabled = !value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.up * floorDistance);
    }
}