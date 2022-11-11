using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputProvider
{
    public override float HandleMoveInput()
    {
        return 1f;
    }
    public override bool HandleJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }
    public override bool HandleJumpReleased()
    {
        return Input.GetButtonUp("Jump");
    }
    public override bool HandleJumpHoldInput()
    {
        return Input.GetButton("Jump");
    }
}
