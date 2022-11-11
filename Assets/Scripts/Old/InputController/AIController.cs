using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputProvider
{
    public override float HandleMoveInput()
    {
        return 1f;
    }
    public override bool HandleJumpHoldInput()
    {
        return false;
    }

    public override bool HandleJumpInput()
    {
        return false;
    }

    public override bool HandleJumpReleased()
    {
        return false;
    }
}
