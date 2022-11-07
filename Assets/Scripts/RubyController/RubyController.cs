using UnityEngine;
using UnityEngine.InputSystem;
using Interfaces;

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
        private bool jumpInputRealesed;

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
            jumpInputRealesed = _controller.inputs.HandleJumpReleased();

            MovementUpdate();
            AnimationUpdate();
            Jump();
            CoyoteTimerCheck();

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
        private void Jump()
        {
            if(jumpInput && coyoteTimeCounter > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpVelocity);
                isJumping = true;
                jumpInput = false;
            }
            if(jumpInputRealesed && _rb.velocity.y > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / jumpRealesedDecreaseVelocity);
                coyoteTimeCounter = 0f;
            }
            else if (_rb.velocity.y < 0)
            {
                isJumping = false;
            }
        }

        private void FixedUpdate()
        {
        }

        public bool isGrounded()
        {
            return _groundCheck.Check();
        }

/*        public void SetHorizontalMove(float value)
        {
            _rb.velocity = new Vector2(value, _rb.velocity.y);
        }*/

        private void MovementUpdate()
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
