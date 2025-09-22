using Constants;
using UnityEngine;

namespace Components
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float PlayerMoveSpeed;
        public float PlayerJumpForce;
        public AttackSettings AttackSettings;
    }
}