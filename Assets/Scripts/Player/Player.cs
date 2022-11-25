using Interfaces;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    #region References
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject groundCheckObject;
    [SerializeField] private LayerMask groundedLayerMask;
    [SerializeField] private LayerMask damageLayers;
    #endregion

    #region State machine variables
    //States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerGlidingState glidingState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    //public PlayerGetHitState playerGetHitState { get; private set; }
    #endregion

    #region Components
    //Components
    public Animator animator { get; private set; }
    public PlayerInputHandler playerInputHandler { get; private set; }
    public Rigidbody2D rb2D { get; private set; }

    public SpriteRenderer spriteRenderer { get; private set; }

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
    public float Health { get { return currentHealth; } }
    public bool isInvincible { get; private set; }
    
    public float timeInvincible = 2.0f;
    private float invincibleTimer;
    private Vector2 moveVector;
    private float currentHealth;

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
        //playerGetHitState = new PlayerGetHitState(this, stateMachine, playerData, "GetHit");

        coll2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _groundCheck = groundCheckObject.GetComponent<ICheck>();

        contactFilter.layerMask = groundedLayerMask;
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = false;
    }
    private void Start()
    {
        facingDirection = 1;
        isInvincible = false;
        currentHealth = playerData.characterMaxHealth;
        stateMachine.Initialize(runState); 
    }

    private void Update()
    {
        currentVelocity = rb2D.velocity;
        stateMachine.currentState.LogicUpdate();
        Invincible();
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
    #endregion

    public void Damage(float amount)
    {
        ChangeHealth(amount);
    }

    private void ChangeHealth(float amount)
    {
        if (amount > 0)
        {
            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, playerData.characterMaxHealth);
            Debug.Log(currentHealth + "Ouch");
        }
    }

    private void Invincible()
    {
        if (coll2D.IsTouchingLayers(damageLayers) && isInvincible)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                spriteRenderer.color = Color.white;
                isInvincible = false;
            }

        }
    }

    private void Death()
    {

    }
    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}
