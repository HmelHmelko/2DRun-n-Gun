using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region References
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject groundCheckObject;
    #endregion

    #region State machine variables
    //States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerGlidingState glidingState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion

    #region Components
    //Components
    public Animator animator { get; private set; }
    public PlayerInputHandler playerInputHandler { get; private set; }
    public Rigidbody2D rb2D { get; private set; }

    #endregion

    #region Interface
    //Interface
    private ICheck _groundCheck;
    #endregion

    #region Methods variables
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    private Vector2 moveVector;
    #endregion

    #region UnityEngine shit
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        runState = new PlayerRunState(this,stateMachine, playerData, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        airState = new PlayerAirState(this, stateMachine, playerData, "inAir");
        glidingState = new PlayerGlidingState(this, stateMachine, playerData, "Glide");
        dashState = new PlayerDashState(this, stateMachine, playerData, "Dash");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>(); 
        rb2D = GetComponent<Rigidbody2D>();
        _groundCheck = groundCheckObject.GetComponent<ICheck>();

        facingDirection = 1;

        stateMachine.Initialize(runState);
    }

    private void Update()
    {
        currentVelocity = rb2D.velocity;
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        moveVector.Set(velocity, currentVelocity.y);
        rb2D.velocity = moveVector;
        currentVelocity = moveVector;
    }

    public void SetVelocityY(float velocity)
    {
        moveVector.Set(currentVelocity.x, velocity);
        rb2D.velocity = moveVector;
        currentVelocity = moveVector;
    }

    public void SetVelocity(float velocityX, float velocityY)
    {
        moveVector.Set(velocityX, velocityY);
        rb2D.velocity = moveVector;
        currentVelocity = moveVector;
    }
    
    public void SetForce(Vector2 force)
    {
        rb2D.AddForce(force, ForceMode2D.Impulse);
    }
    #endregion

    #region Check Functions
    public bool isGrounded()
    {
        return _groundCheck.Check();
    }
    #endregion

    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
