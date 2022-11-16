using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region PublicGetPrivateSetVariables
    public float moveInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool glidingInput { get; private set; }
    public bool dashInput { get; private set; }

    public bool[] ShootInputs { get; private set; }
    #endregion

    #region localVariables
    [Header("Jump Buffer")]
    [SerializeField] private float inputHoldTime = 0.2f;

    [Header("Glide Delay")]
    [SerializeField] private float inputHoldTimerBeforeStartGlide = 0.2f;
    private float jumpInputStartTime;
    private bool glideInputStart;
    private float glideInputCounterTimer;
    #endregion

    #region UnityEngine Shit
    private void Start()
    { 
        int count = Enum.GetValues(typeof(ShootInputsEnum)).Length;
        ShootInputs = new bool[count];
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckGlideInputHoldTime();
    }

    public float OnMoveInput()
    {
        return moveInput = 1f;
    }
    #endregion

    public void OnPrimaryShootInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            ShootInputs[(int)ShootInputsEnum.Primary] = true;
        }

        if(context.canceled)
        {
            ShootInputs[(int)ShootInputsEnum.Primary] = false;
        }

    }
    public void OnSecondaryShootInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShootInputs[(int)ShootInputsEnum.Secondary] = true;
        }

        if (context.canceled)
        {
            ShootInputs[(int)ShootInputsEnum.Secondary] = false;
        }

    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            jumpInputStartTime = Time.time;
            jumpInputStop = false;
        }
        if (context.canceled)
        {
            jumpInputStop = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dashInput = true;
        }
        else if (context.canceled)
        {
            dashInput = false;
        }
    }
    public void OnGlideInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            glideInputStart = true;
        }
        else if (context.canceled)
        {
            glidingInput = false;
            glideInputStart = false;
        }
    }
    
    private void CheckGlideInputHoldTime()
    {
        if(glideInputStart)
        {
            glideInputCounterTimer -= Time.deltaTime;
            if(glideInputCounterTimer <= 0.01f)
            {
                glidingInput = true;
            }
        }
        else
        {
            glideInputCounterTimer = inputHoldTimerBeforeStartGlide;
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }

    public void UseGlideInput() => glidingInput = false;
    public void UseJumpInput() => jumpInput = false;
    public void UseDashInput() => dashInput = false;
}

public enum ShootInputsEnum
{
    Primary,
    Secondary
}