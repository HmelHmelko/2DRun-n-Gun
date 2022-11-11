using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Run State")]
    public float movementVelocity = 5.0f;

    [Header("Jump State")]
    public float jumpVelocity = 5.0f;
    public int numberOfJumps = 1;

    [Header("Dash State")]
    public float dashTime = 5.0f;
    public float dashVelocityMultiplier = 2.0f;
    public float dashCooldown = 5.0f;

    [Header("Air State")]
    public float jumpRealesedDecreaseVelocity = 2.0f;
    public float coyoteTime = 0.2f;
    public float jumpVelocityMultiplier = 0.5f;
}
