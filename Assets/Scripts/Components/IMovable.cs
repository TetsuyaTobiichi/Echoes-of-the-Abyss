
using UnityEngine;

namespace Components
{
    public interface IMovable
    {
        public float MoveSpeed { get; }
        public Vector2 MoveDirection { get; }
        public bool IsMoving { get; }
        public float JumpForce { get; }
        public bool IsGrounded { get; }
    }
}