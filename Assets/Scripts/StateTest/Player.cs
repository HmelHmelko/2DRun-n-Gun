using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region References
    [SerializeField]
    private PlayerData playerData;
    public GameObject groundCheckObject;
    #endregion

    #region State machine variables
    //States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerGlidingState glidingState { get; private set; }
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
        glidingState = new PlayerGlidingState(this, stateMachine, playerData, "Gliding");
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
    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int input)
    {
        if(input != 0 && input != facingDirection)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingDirection *= 1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public bool isGrounded()
    {
        return _groundCheck.Check();
    }
    #endregion

    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
