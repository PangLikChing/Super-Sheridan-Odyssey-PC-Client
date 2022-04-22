using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public float speed;
    public float speedChangeRate = 1.0f;
    public float initialFacing;
    public float jumpHeight;
    public float gravityModifier = 2.0f;
    public float groundCheckerRadius;
    public LayerMask groundLayers;
    public Vector3 groundingOffset;

    private CharacterController controller;
    private Animator animator;
    private GameObject mainCamera;

   
    private Vector3 velocity;
    private Vector3 vertical;
    private float currentSpeed;
    private float targetRotation = 0.0f;
    private float rotationSmoothFactor = 0.05f;
    [SerializeField]
    private bool isJumping;
    private bool isGrounded;

    private int animIDSpeed;
    private int animIDGrounded;
    private int animIDJump;
    private int animIDVictory;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Start is called before the first frame update
    void Start()
    {
        AssignAnimationIDs();
        isGrounded = false;
    }

    void Update()
    {
        GroundedCheck();
        Gravity();
        Move(PlayerController.Instance.inputHandler.move);
    }

    private void AssignAnimationIDs()
    {
        animIDJump = Animator.StringToHash("Jump");
        animIDSpeed = Animator.StringToHash("Speed");
        animIDVictory = Animator.StringToHash("Victory");
        animIDGrounded = Animator.StringToHash("Grounded");
    }

    void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundingOffset, groundCheckerRadius, groundLayers, QueryTriggerInteraction.Ignore);
        animator.SetBool(animIDGrounded, isGrounded);
    }

    void Gravity()
    {
        vertical.y += gravityModifier * Physics.gravity.y * Time.deltaTime;
        if (isGrounded && vertical.y < 0)
        {
            vertical.y = 0;
        }
    }

    public void Jump()
    {
        if (isGrounded&&!isJumping)
        {
            animator.SetTrigger(animIDJump);
            isJumping = true;
        }
    }
    public void JumpDisplacement()
    {
        vertical.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
    }
    public void JumpComplete()
    {
        isJumping = false;   
    }
    public void Move(Vector2 move)
    {
        Vector3 targetDirection = Vector3.zero;
        float targetSpeed = 0;

        if (move != Vector2.zero&&!isJumping)
        {
            //Debug.LogError(mainCamera);
            targetRotation = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            targetSpeed = speed;
        }

        transform.rotation = Quaternion.Euler(0.0f, Mathf.LerpAngle(transform.eulerAngles.y, targetRotation, rotationSmoothFactor), 0.0f);

        if (currentSpeed != targetSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);
        }
        
        targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        if (!isJumping)
        {
            velocity = targetDirection.normalized * currentSpeed + vertical;
        }
        else 
        {
            velocity.y = vertical.y;
        }
        controller.Move(velocity * Time.deltaTime);
        animator.SetFloat(animIDSpeed, currentSpeed);
    }

    public void OnVictory()
    {
        animator.SetBool(animIDVictory, true);
        this.enabled = false;
    }

    private void OnEnable()
    {
        velocity = Vector3.zero;
        vertical = Vector3.zero;
        targetRotation = initialFacing;
        isJumping = false;
    }

    private void OnDrawGizmos()
    {
        // draw ground checker
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + groundingOffset, groundCheckerRadius);
    }
}
