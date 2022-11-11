using System.Collections;
using System.Collections.Generic;
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
    public bool dashInputStop { get; private set; }
    #endregion

    #region localVariables
    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float dashInputAfterTime;
    #endregion

    #region UnityEngine Shit
    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public float OnMoveInput()
    {
        return moveInput = 1f;
    }
    #endregion

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            jumpInputStartTime = Time.time;
            jumpInputStop = false;
        }
        if(context.canceled)
        {
            jumpInput = false;
            jumpInputStop = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            dashInput = true;
            dashInputStop = false;
        }
        else if(context.canceled)
        {
            dashInput = false;
            dashInputStop = true;
        }
    }
    
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }
    public void UseJumpInput() => jumpInput = false;
    public void UseDashInput() => dashInput = false;
}
