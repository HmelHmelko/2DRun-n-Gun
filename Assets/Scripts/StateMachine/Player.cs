using Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    #region References
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject groundCheckObject;
    [SerializeField] private LayerMask groundedLayerMask;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform shootPosition;
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
    public Transform currentShootPosition { get; private set; }

    CapsuleCollider2D coll2D;
    ContactFilter2D contactFilter;
    RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
    RaycastHit2D[] foundHits = new RaycastHit2D[3];
    Collider2D[] groundColliders = new Collider2D[3];
    Vector2[] raycastPositions = new Vector2[3];
    #endregion

    #region Interface
    //Interface
    private ICheck _groundCheck;
    #endregion

    #region Methods variables
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    private Vector2 moveVector;
    //private Vector2 previousPosition;
    //private Vector2 currentPosition;

    #endregion

    #region UnityEngine shit
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        runState = new PlayerRunState(this, stateMachine, playerData, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        airState = new PlayerAirState(this, stateMachine, playerData, "inAir");
        glidingState = new PlayerGlidingState(this, stateMachine, playerData, "Glide");
        dashState = new PlayerDashState(this, stateMachine, playerData, "Dash");

        coll2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rb2D = GetComponent<Rigidbody2D>();
        _groundCheck = groundCheckObject.GetComponent<ICheck>();

        currentShootPosition = shootPosition;

        contactFilter.layerMask = groundedLayerMask;
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = false;
    }

    private void Start()
    {
        weaponData.shotsTimer = 0.0f;
        facingDirection = 1;
        stateMachine.Initialize(runState); 
    }

    private void Update()
    {
        Shoot();
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

    #region Other Functions

    public void Shoot()
    {
        if (playerInputHandler.ShootInputs[(int)ShootInputsEnum.Primary])
        {
            if (Time.time >= weaponData.shotsTimer)
            {
                weaponData.shotsTimer = Time.time + 1f /weaponData.shotsPerSecond;
                SpawnBullet();
            }
        }
    }

    private void SpawnBullet()
    {
        GameObject newBullet = Instantiate(weaponData.bulletPrefab, currentShootPosition.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(weaponData.bulletSpeed, 0.0f);
    }



        #endregion
    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
