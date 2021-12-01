using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 0.05f;
    private Vector3 currentMovement;

    [Header("Jumping")]
    [SerializeField] private float maxJumpHeight = 3.2f;
    [SerializeField] private float maxJumpTime = 1f;
    [SerializeField] private float fallMultiplier = 2f;
    private float gravityValue = -9.81f; // not final gravity !
    private float groundGravity = -0.05f; // Ensure collider is grounded
    private float initialJumpVelocity;
    private int jumpCount = 0;

    [Header("Turning")]
    [SerializeField] private float trackDistance = 2.5f; //The distance between tracks
    private int currentTrack = 1; // 0:Left Middle, 1:Middle, 2:Right Middle
    private float speedBetweenTrack = 5f;

    [Header("Sliding")]
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private float newCenter = 0.35f;
    [SerializeField] private float newHeight = 0.4f;
    private bool isSliding;
    private int slideCount = 0;

    [Header(("Dying"))]
    private bool isDying;
    private int dyingCount = 0;

    [Header("UnityStuff")]
    private CharacterController controller;
    private Animator animator;
    private PlayerInputs inputScheme;
    private Vector3 controllerCenter;
    private float controllerHeight;
    private QuitHandler quitHandler;

    void Awake()
    {
        inputScheme = new PlayerInputs();
        quitHandler = new QuitHandler(inputScheme.Player.Quit);
        setUpJumpVariables();
    }

    void OnEnable()
    {
        inputScheme.Enable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        controllerCenter = controller.center;
        controllerHeight = controller.height;
    }

    // set up jump variables physically
    private void setUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravityValue = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    // Handle moving, jumping, sliding and turning
    void Update()
    {
        if (!MenuManager.isGameStarted)
        {
            return;
        }
        if (controller)
        {
            HandleJumping();
            HandleSlide();
            HandleMovement();
            HandleTurning();
        }
        HandleFalling();
        controller.Move(currentMovement * Time.deltaTime);
        HandleGravity();
    }

    void HandleFalling()
    {
        if (!controller.isGrounded && inputScheme.Player.Slide.WasPressedThisFrame())
        {
            currentMovement.y = -15;
        }
    }

    // Accelerate and set the current movement direction (z)
    private void HandleMovement()
    {
        Accelerate();
        currentMovement.z = moveSpeed;
    }

    // Handle jump
    private void HandleJumping()
    {
        if (inputScheme.Player.Jump.WasPressedThisFrame() && controller.isGrounded)
        {
            jumpCount += 1;
            animator.SetInteger("jumpCount", jumpCount);
            Jump();
        }
    }

    // set up jump height for current movement direction (y)
    private void Jump()
    {
        currentMovement.y = initialJumpVelocity * 0.75f;
        animator.SetBool("IsJumping", true);
        animator.SetBool("IsSliding", false);
    }

    // Handle gravity on ground and on jump
    private void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f;
        if (controller.isGrounded)
        {
            animator.SetBool("IsJumping", false);
            currentMovement.y = groundGravity;
            if (jumpCount == 4)
            {
                jumpCount = 0;
                animator.SetInteger("jumpCount", jumpCount);
            }
        }else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = previousYVelocity + (gravityValue * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = previousYVelocity + (gravityValue * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
        }
    }


    // Handle turning and interpolate future position (x)
    private void HandleTurning()
    {
        // Calculate track
        if (inputScheme.Player.TurnLeft.WasPressedThisFrame())
        {
            currentTrack--;
            if (currentTrack == -1)
                currentTrack = 0;
        }
        else if (inputScheme.Player.TurnRight.WasPressedThisFrame())
        {
            currentTrack++;
            if (currentTrack == 3)
                currentTrack = 2;
        }

        // Calculate future position
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (currentTrack == 0)
        {
            targetPosition += Vector3.left * trackDistance;
        }
        else if (currentTrack == 2)
        {
            targetPosition += Vector3.right * trackDistance;

        }
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * speedBetweenTrack * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                controller.Move(moveDir);
            }
            else
            {
                controller.Move(diff);
            }
        }
    }

    // Handle slide
    private void HandleSlide()
    {
        if (inputScheme.Player.Slide.WasPressedThisFrame() && !isSliding && controller.isGrounded)
        {
            StartCoroutine(StartSlide());
        }
    }

    IEnumerator StartSlide()
    {
        isSliding = true;
        slideCount += 1;
        if (slideCount >= 5)
        {
            slideCount = 1;
        }
        animator.SetInteger("slideCount", slideCount);
        animator.SetBool("IsSliding", true);
        yield return new WaitForSeconds(0.25f / Time.timeScale);
        controller.center = new Vector3(0, newCenter, 0);
        controller.height =newHeight;
        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);
        animator.SetBool("IsSliding", false);
        controller.center = controllerCenter;
        controller.height = controllerHeight;
        isSliding = false;
    }

    private void Accelerate()
    {
        if (moveSpeed <= maxSpeed)
        {
            moveSpeed += acceleration * Time.deltaTime;
        }
    }

    // Handle collision
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Fence")
        {
            if (!isDying)
            {
                Die();
            }
        }

        if (hit.collider.tag == "Car")
        {
            RaycastHit hitter;
            // Collision from front
            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hitter, 1f))
            {
                if (hitter.collider.gameObject.tag == "Car" && hit.transform.position.z > transform.position.z)
                {
                    if (!isDying)
                    {
                        Die();
                    }
                }
            }
            // Collision from left
            if (transform.position.x > hit.transform.position.x)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.left, out hitter, 1f))
                {
                    if (hitter.collider.gameObject.tag == "Car" && hitter.collider.name == hit.collider.name)
                    {
                        if (!isDying)
                        {
                            Die();
                        }
                    }
                }
            }
            // Collision from right
            if (transform.position.x < hit.transform.position.x)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.right, out hitter, 1f))
                {
                    if (hitter.collider.gameObject.tag == "Car" && hitter.collider.name == hit.collider.name)
                    {
                        if (!isDying)
                        {
                            Die();
                        }
                    }
                }
            }
        }
    }

    private void Die()
    {
        MenuManager.gameOver = true;
        moveSpeed = 0;
        acceleration = 0;
        dyingCount = Random.Range(1, 7);
        animator.SetInteger("dyingCount", dyingCount);
        animator.SetTrigger("IsDying");
        isDying = true;
    }
}
