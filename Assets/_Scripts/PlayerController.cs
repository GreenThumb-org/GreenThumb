using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float groundAcceleration = 15f;
    [SerializeField] private float groundDeceleration = 20f;
    [SerializeField] private float airAcceleration = 2.5f;
    [SerializeField] private float maxSlopeAngle = 45f;

    [Header("Looking")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 89f;

    [Header("Jumping & Gravity")]
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float jumpBufferTime = 0.15f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpCooldown = 0.25f;

    [Header("Ground Detection")]
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private float xRotation;
    private float yRotation;

    private Vector2 moveInput;
    private bool isGrounded;
    private RaycastHit slopeHit;

    private float lastGroundedTime;
    private float lastJumpPressedTime;
    private float lastJumpExecutedTime;

    private bool canControl = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.useGravity = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        TabletManager.OnTabletToggled += HandleMenuToggle;
    }

    private void OnDisable()
    {
        TabletManager.OnTabletToggled -= HandleMenuToggle;
    }

    private void Update()
    {
        HandleInput();
        if (!canControl) return;
        HandleLook();
        UpdateTimers();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
        HandleGravity();

        if (canControl)
            HandleJump();
    }

    private void HandleMenuToggle(bool isMenuOpen)
    {
        canControl = !isMenuOpen;
        if (isMenuOpen)
            moveInput = Vector2.zero;
    }

    private void HandleInput()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetButtonDown("Jump"))
            lastJumpPressedTime = Time.time;
    }

    private void UpdateTimers()
    {
        if (isGrounded)
            lastGroundedTime = Time.time;
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        yRotation += mouseX;

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        rb.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void CheckGrounded()
    {
        Vector3 origin = transform.position;
        float castDistance = (playerHeight * 0.5f) - groundCheckRadius + 0.15f;
        isGrounded = Physics.SphereCast(origin, groundCheckRadius, Vector3.down, out slopeHit, castDistance, groundMask);
    }

    private bool OnSlope()
    {
        if (isGrounded && slopeHit.normal != Vector3.up)
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void HandleMovement()
    {
        Vector3 moveDir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) && canControl ? sprintSpeed : walkSpeed;

        if (moveInput.sqrMagnitude == 0)
            targetSpeed = 0f;

        Vector3 targetVelocity = moveDir * targetSpeed;
        Vector3 currentVelocity = rb.linearVelocity;

        if (isGrounded && OnSlope())
        {
            targetVelocity = GetSlopeMoveDirection(moveDir) * targetSpeed;
            if (currentVelocity.y < 0 && moveInput.sqrMagnitude > 0)
                rb.AddForce(Vector3.down * 15f, ForceMode.Acceleration);
        }
        else
            targetVelocity.y = currentVelocity.y;

        Vector3 velocityDiff = targetVelocity - currentVelocity;
        float accelRate = isGrounded ? (targetSpeed > 0.01f ? groundAcceleration : groundDeceleration) : airAcceleration;

        rb.AddForce(velocityDiff * accelRate, ForceMode.Acceleration);
    }

    private void HandleGravity()
    {
        if (isGrounded && OnSlope() && moveInput.sqrMagnitude == 0f)
            return;

        float gravityMultiplier = (!isGrounded && rb.linearVelocity.y < 0) ? fallMultiplier : 1f;
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
    }

    private void HandleJump()
    {
        bool bufferValid = Time.time - lastJumpPressedTime <= jumpBufferTime;
        bool coyoteValid = Time.time - lastGroundedTime <= coyoteTime;
        bool cooldownValid = Time.time - lastJumpExecutedTime >= jumpCooldown;

        if (bufferValid && coyoteValid && cooldownValid)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            lastJumpPressedTime = 0f;
            lastGroundedTime = 0f;
            lastJumpExecutedTime = Time.time;
            isGrounded = false;
        }
    }
}