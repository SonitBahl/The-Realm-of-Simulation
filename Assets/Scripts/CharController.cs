using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    private bool canJump = true;
    private float jumpCooldownTimer = 0f;
    public Transform groundCheck; // Remove the ground check

    [Header("Rigidbody")]
    public Rigidbody rb;

    void Start()
    {
        // If animator or rigidbody are not assigned, try to find them on the GameObject
        if (animator == null)
            animator = GetComponent<Animator>();
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input for movement
        float moveInputVertical = Input.GetAxis("Vertical");
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Set animator triggers based on movement input
        if (moveInputVertical != 0 || moveInputHorizontal != 0)
        {
            animator.ResetTrigger("walkback");
            animator.SetTrigger("walk");
            if (isRunning)
                animator.SetTrigger("run");
        }
        else
        {
            animator.ResetTrigger("walk");
            animator.ResetTrigger("walkback");
            animator.ResetTrigger("run");
        }

        // Jump cooldown
        if (!canJump)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= 5f) // Adjust cooldown duration here
            {
                canJump = true;
                jumpCooldownTimer = 0f;
            }
        }

        // Trigger jump animation and apply force if jump is allowed and jump key is pressed
        if (canJump && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false; // Prevent jumping until cooldown is over
        }
    }

    void FixedUpdate()
    {
        // Move the character
        float moveInputVertical = Input.GetAxis("Vertical");
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 moveDirection = new Vector3(moveInputHorizontal, 0, moveInputVertical).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, newRotation, 10f * Time.fixedDeltaTime));
        }

        Vector3 movement = moveDirection;
        if (isRunning)
        {
            movement *= runSpeed;
        }
        else
        {
            movement *= walkSpeed;
        }

        rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }
}
