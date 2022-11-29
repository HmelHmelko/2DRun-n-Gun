using Interfaces;
using System.Collections;
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
    #endregion

    #region Interface
    #endregion

    #region Methods variables
    public bool IsGrounded { get; protected set; }
    public bool IsCeilinged { get; protected set; }
    public Collider2D[] GroundColliders { get { return groundColliders; } }
    public ContactFilter2D ContactFilter { get { return contactFilter; } }
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }
    public float Health { get { return currentHealth; } }
    public bool isInvincible { get; private set; }

    public float groundedRaycastDistance = 0.1f;

    CapsuleCollider2D coll2D;
    ContactFilter2D contactFilter;
    RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
    RaycastHit2D[] foundHits = new RaycastHit2D[3];
    Collider2D[] groundColliders = new Collider2D[3];
    Vector2[] raycastPositions = new Vector2[3];

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

        contactFilter.layerMask = groundedLayerMask;
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = false;

        Physics2D.queriesStartInColliders = false;
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
        CheckCapsuleEndCollisions();
        CheckCapsuleEndCollisions(false);
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
    private void CheckCapsuleEndCollisions(bool bottom = true)
    {
        Vector2 raycastDirection;
        Vector2 raycastStart;
        float raycastDistance;

        if (coll2D == null)
        {
            raycastStart = rb2D.position + Vector2.up;
            raycastDistance = 1f + groundedRaycastDistance;

            if (bottom)
            {
                raycastDirection = Vector2.down;

                raycastPositions[0] = raycastStart + Vector2.left * 0.4f;
                raycastPositions[1] = raycastStart;
                raycastPositions[2] = raycastStart + Vector2.right * 0.4f;
            }
            else
            {
                raycastDirection = Vector2.up;

                raycastPositions[0] = raycastStart + Vector2.left * 0.4f;
                raycastPositions[1] = raycastStart;
                raycastPositions[2] = raycastStart + Vector2.right * 0.4f;
            }
        }
        else
        {
            raycastStart = rb2D.position + coll2D.offset;
            raycastDistance = coll2D.size.x * 0.5f + groundedRaycastDistance * 2f;

            if (bottom)
            {
                raycastDirection = Vector2.down;
                Vector2 raycastStartBottomCentre = raycastStart + Vector2.down * (coll2D.size.y * 0.5f - coll2D.size.x * 0.5f); 

                raycastPositions[0] = raycastStartBottomCentre + Vector2.left * coll2D.size.x * 0.5f;
                raycastPositions[1] = raycastStartBottomCentre;
                raycastPositions[2] = raycastStartBottomCentre + Vector2.right * coll2D.size.x * 0.5f;
            }
            else
            {
                raycastDirection = Vector2.up;
                Vector2 raycastStartTopCentre = raycastStart + Vector2.up * (coll2D.size.y * 0.5f - coll2D.size.x * 0.5f);

                raycastPositions[0] = raycastStartTopCentre + Vector2.left * coll2D.size.x * 0.5f;
                raycastPositions[1] = raycastStartTopCentre;
                raycastPositions[2] = raycastStartTopCentre + Vector2.right * coll2D.size.x * 0.5f;
            }
        }

        for (int i = 0; i < raycastPositions.Length; i++)
        {
            int count = Physics2D.Raycast(raycastPositions[i], raycastDirection, contactFilter, hitBuffer, raycastDistance);

            if (bottom)
            {
                foundHits[i] = count > 0 ? hitBuffer[0] : new RaycastHit2D();
                groundColliders[i] = foundHits[i].collider;
            }
            else
            {
                IsCeilinged = false;
            }
        }

        if (bottom)
        {
            Vector2 groundNormal = Vector2.zero;
            int hitCount = 0;

            for (int i = 0; i < foundHits.Length; i++)
            {
                if (foundHits[i].collider != null)
                {
                    groundNormal += foundHits[i].normal;
                    hitCount++;
                }
            }

            if (hitCount > 0)
            {
                groundNormal.Normalize();
            }

            Vector2 relativeVelocity = currentVelocity;
            for (int i = 0; i < groundColliders.Length; i++)
            {
                if (groundColliders[i] == null)
                    continue;
            }

            if (Mathf.Approximately(groundNormal.x, 0f) && Mathf.Approximately(groundNormal.y, 0f))
            {
                IsGrounded = false;
            }
            else
            {
                IsGrounded = relativeVelocity.y <= 0f;

                if (coll2D != null)
                {
                    if (groundColliders[1] != null)
                    {
                        float capsuleBottomHeight = rb2D.position.y + coll2D.offset.y - coll2D.size.y * 0.5f;
                        float middleHitHeight = foundHits[1].point.y;
                        IsGrounded &= middleHitHeight < capsuleBottomHeight + groundedRaycastDistance;
                    }
                }
            }
        }

        for (int i = 0; i < hitBuffer.Length; i++)
        {
            hitBuffer[i] = new RaycastHit2D();
        }
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
            invincibleTimer = playerData.timeInvincible;
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
