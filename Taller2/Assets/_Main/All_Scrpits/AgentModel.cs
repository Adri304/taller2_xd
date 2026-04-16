using UnityEngine;

public class PlayerMovementModel : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private PlayerPowerUps playerPowerUps;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    public bool IsGrounded => isGrounded;

    public Vector3 CurrentHorizontalVelocity { get; private set; }
    public float CurrentSpeed { get; private set; }
    public Vector3 CurrentMoveDirection { get; private set; }

    private void Start()
    {
        if (playerInputController == null)
            Debug.LogError("Falta PlayerInputController");

        if (rb == null)
            Debug.LogError("Falta Rigidbody");
    }

    private void FixedUpdate()
    {
        CheckGround();   // 👈 nuevo
        Move();
        Jump();          // 👈 nuevo
        UpdateVelocityData();
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }

    private void Move()
    {
        if (playerInputController == null || rb == null) return;

        Vector2 input = playerInputController.MoveInput;

        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);

        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
            CurrentMoveDirection = moveDirection;

        float speed = playerPowerUps != null ? playerPowerUps.GetVelocidad() : moveSpeed;

        Vector3 newVelocity = new Vector3(
            moveDirection.x * speed,
            rb.linearVelocity.y,
            moveDirection.z * speed
        );

        rb.linearVelocity = newVelocity;
    }

    private void Jump()
    {
        if (playerInputController == null || rb == null) return;

        if (playerInputController.JumpPressed && isGrounded)
        {
            // Reinicia velocidad vertical para un salto limpio
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Debug.Log("🦘 Salto ejecutado");
        }
    }

    private void UpdateVelocityData()
    {
        if (rb == null) return;

        Vector3 horizontalVelocity = rb.linearVelocity;
        horizontalVelocity.y = 0f;

        CurrentHorizontalVelocity = horizontalVelocity;
        CurrentSpeed = horizontalVelocity.magnitude;
    }
}