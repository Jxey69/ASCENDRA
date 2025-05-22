using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask climbableMask;

    private Rigidbody rb;
    private PlayerInput input;
    private Vector2 moveInput;
    private bool isSprinting;
    private bool isClimbing;
    private bool requestJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        input = new PlayerInput();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => moveInput = Vector2.zero;

        input.Player.Sprint.performed += _ => isSprinting = true;
        input.Player.Sprint.canceled += _ => isSprinting = false;

        input.Player.Jump.performed += _ => requestJump = true;

        input.Player.Enable();
    }

    private void Update()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        orientation.forward = forward.normalized;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            Climb();
        }
        else
        {
            Move();

            if (requestJump && IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (requestJump && !IsGrounded() && CanClimb())
            {
                isClimbing = true;
                rb.useGravity = false;
            }
        }

        requestJump = false;
    }

    private void Move()
    {
        Vector3 direction = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        if (direction.sqrMagnitude > 0.01f)
        {
            // Perform a short sweep in movement direction
            RaycastHit hit;
            Vector3 movement = direction.normalized;
            if (Physics.Raycast(transform.position, movement, out hit, 0.6f, groundMask))
            {
                // Slide along the wall
                Vector3 wallNormal = hit.normal;
                movement = Vector3.ProjectOnPlane(movement, wallNormal).normalized;
            }

            Vector3 velocity = movement * speed;

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);

            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }


    private void Climb()
    {
        Vector3 climbDirection = orientation.up * moveInput.y + orientation.right * moveInput.x;
        rb.linearVelocity = climbDirection * climbSpeed;
    }

    private bool IsGrounded()
    {
        float radius = 0.3f;
        float height = 1.0f;
        Vector3 center = transform.position + Vector3.up * 0.5f;
        return Physics.CheckCapsule(center + Vector3.up * (height * 0.5f - radius), center - Vector3.up * (height * 0.5f - radius), radius, groundMask);
    }

    private bool CanClimb()
    {
        return Physics.CheckSphere(transform.position, 0.5f, climbableMask);
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & climbableMask) != 0)
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float radius = 0.3f;
        float height = 1.0f;
        Vector3 center = transform.position + Vector3.up * 0.5f;
        Vector3 top = center + Vector3.up * (height * 0.5f - radius);
        Vector3 bottom = center - Vector3.up * (height * 0.5f - radius);
        Gizmos.DrawWireSphere(top, radius);
        Gizmos.DrawWireSphere(bottom, radius);
    }
#endif
}
