using Constants;
using UnityEngine;

namespace Components
{
    public struct Player
    {
        public Rigidbody2D PlayerRigidbody;
        public float PlayerMoveSpeed;
        public float PlayerJumpForce;
        public AttackSettings AttackSettings;
    }
}