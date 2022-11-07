using Interfaces;
using UnityEngine;

namespace Utility
{
    public class GroundCheck : MonoBehaviour, ICheck
    {
        [Header("Capsule Ground Check Settings")]
        [SerializeField] float width = 0.1f;
        [SerializeField] float height = 0.1f;
        [SerializeField] LayerMask groundLayer;

        public bool Check()
        {
            return Physics2D.OverlapCapsule(transform.position, new Vector2(width, height),CapsuleDirection2D.Horizontal, 0, groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1));
        }
    }
}
