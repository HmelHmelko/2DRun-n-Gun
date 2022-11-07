using UnityEngine;

//Svoeobraznii interface dlya inputov
public abstract class InputProvider : ScriptableObject
{
    public abstract float HandleMoveInput();
    public abstract bool HandleJumpInput();
    public abstract bool HandleJumpReleased();
    public abstract bool HandleJumpHoldInput();
}
