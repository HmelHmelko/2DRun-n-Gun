using UnityEngine;
using Interfaces;
using UnityEditorInternal;

namespace RubyGameplay
{
    public class RubyController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] float movementSpeed = 5.0f;

        [Header("Jump Settings")]
        [SerializeField] float jumpVelocity = 4.0f;
        [SerializeField] float jumpRealesedDecreaseVelocity = 2.0f;
        [SerializeField] float coyoteTime = 0.2f;
        private float coyoteTimeCounter;
        private bool isJumping;
        private bool jumpInput;
        private bool jumpInputReleased;

        [Header("Ground check")]
        [SerializeField] private GameObject groundCheckObject;

        //Interfaces
        private ICheck _groundCheck;

        private Controller _controller;
        private Rigidbody2D _rb;
        private Collider2D _collider;
        private Animator _animator;

        private int grounded;
        private int inAir;
        private int jumping;

        public Vector2 _velocity { get; protected set; }
        public Rigidbody2D _rubyRigidbody2D { get { return _rb; } }

        private void Awake()
        {
            //Components
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            _controller = GetComponent<Controller>();
            _groundCheck = groundCheckObject.GetComponent<ICheck>();
        }
        private void Start()
        {
            //Animations
            grounded = Animator.StringToHash("Grounded");
            inAir = Animator.StringToHash("InAir");
            jumping = Animator.StringToHash("Jump");

            //Gravity
        }
        private void Update()
        {
            //Inputs Check
            jumpInput = _controller.inputs.HandleJumpInput();
            jumpInputReleased = _controller.inputs.HandleJumpReleased();

            AnimationUpdate();
            Jump();
            AirGliding();
            CoyoteTimerCheck();
            MovementUpdate();
        }

        private void FixedUpdate()
        {
        }

        //Time before player start falling and still can jump
        private void CoyoteTimerCheck()
        {
            if (isGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
        }

        private void AirGliding()
        {
            if(jumpInputReleased && !isGrounded())
            {
                //Debug.Log("We can gliding");
                if(Input.GetButtonDown("Jump"))
                {
                    _rb.gravityScale = 0;
                    //Debug.Log("actually gliding");
                }
            }
        }
        
        private void Jump()
        {
            if(jumpInput && coyoteTimeCounter > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpVelocity);
                isJumping = true;
                jumpInput = false;
            }
            if(jumpInputReleased && _rb.velocity.y > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / jumpRealesedDecreaseVelocity);
                coyoteTimeCounter = 0f;
            }
            else if (_rb.velocity.y < 0)
            {
                isJumping = false;
            }
        }

        public bool isGrounded()
        {
            return _groundCheck.Check();
        }

        public void MovementUpdate()
        {
            _rb.velocity = new Vector2(movementSpeed, _rb.velocity.y);
        }

        private void AnimationUpdate()
        {
            _animator.SetBool(grounded, isGrounded());
            _animator.SetBool(inAir, !isGrounded());
            _animator.SetBool(jumping, isJumping);
        }
    }
}
