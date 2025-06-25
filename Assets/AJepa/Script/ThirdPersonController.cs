using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private float groundCheckOffset = 0.5f;
    [SerializeField] private LayerMask groundMask; // Set this to your platform layers

    [Header("Climbing")]
    [SerializeField] private float mantleDuration = 0.4f;
    [SerializeField] private float ledgeCheckDistance = 0.6f;
    [SerializeField] private float ledgeCheckHeight = 1.2f;
    [SerializeField] private float ledgeGrabHeight = 1.5f;
    [SerializeField] private LayerMask climbableMask;
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private PlayerControls inputActions;
    private Vector2 moveInput;
    private bool runInput;
    private bool jumpInput;
    private bool isGrounded;
    private bool isMantling;
    private Vector3 mantleTarget;
    private float mantleTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        inputActions = new PlayerControls();
        inputActions.PlayerLocomotionMap.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerLocomotionMap.Movement.canceled += _ => moveInput = Vector2.zero;
        inputActions.PlayerLocomotionMap.Run.performed += _ => runInput = true;
        inputActions.PlayerLocomotionMap.Run.canceled += _ => runInput = false;
        inputActions.PlayerLocomotionMap.Jump.performed += _ => jumpInput = true;
        inputActions.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        orientation.forward = forward.normalized;

        Vector3 groundCheckPos = transform.position + Vector3.down * groundCheckOffset;
        isGrounded = Physics.CheckSphere(groundCheckPos, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);

        if (animator)
        {
            animator.SetBool("isGrounded", isGrounded);

            bool isMoving = moveInput.magnitude > 0.1f;
            bool isLeft = moveInput.x < -0.1f;
            bool isRight = moveInput.x > 0.1f;

            animator.SetBool("isWalking", isMoving && !runInput && isGrounded && !isMantling);
            animator.SetBool("isRunning", isMoving && runInput && isGrounded && !isMantling);
            animator.SetBool("isWalkLeft", isLeft && !runInput && isGrounded && !isMantling);
            animator.SetBool("isWalkRight", isRight && !runInput && isGrounded && !isMantling);
            animator.SetBool("isRunLeft", isLeft && runInput && isGrounded && !isMantling);
            animator.SetBool("isRunRight", isRight && runInput && isGrounded && !isMantling);
        }
    }

    private void FixedUpdate()
    {
        if (isMantling)
        {
            MantleLerp();
            return;
        }

        Vector3 moveDir = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        float speed = runInput ? runSpeed : walkSpeed;
        Vector3 velocity = moveDir.normalized * speed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        Vector3 lookDir = orientation.forward;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 0.2f);

        if (jumpInput)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                if (animator) animator.SetTrigger("isJumping");
            }
            else if (CheckForLedge(out Vector3 ledgePos))
            {
                StartMantle(ledgePos);
            }
            jumpInput = false;
        }
    }

    private bool CheckForLedge(out Vector3 ledgePos)
    {
        Vector3 origin = transform.position + Vector3.up * ledgeCheckHeight;
        Vector3 dir = orientation.forward;
        if (Physics.Raycast(origin, dir, out RaycastHit wallHit, ledgeCheckDistance, climbableMask))
        {
            Vector3 topOrigin = wallHit.point + Vector3.up * ledgeGrabHeight;
            if (Physics.Raycast(topOrigin, Vector3.down, out RaycastHit topHit, ledgeGrabHeight + 0.1f, climbableMask))
            {
                ledgePos = topHit.point + Vector3.up * 0.5f;
                return true;
            }
        }
        ledgePos = Vector3.zero;
        return false;
    }

    private void StartMantle(Vector3 targetPos)
    {
        isMantling = true;
        mantleTarget = targetPos;
        mantleTimer = 0f;
        rb.isKinematic = true;
        if (animator) animator.SetBool("isClimbing", true);
    }

    private void MantleLerp()
    {
        mantleTimer += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(mantleTimer / mantleDuration);
        transform.position = Vector3.Lerp(transform.position, mantleTarget, t);

        if (t >= 1f)
        {
            isMantling = false;
            rb.isKinematic = false;
            if (animator) animator.SetBool("isClimbing", false);
        }
    }
}
