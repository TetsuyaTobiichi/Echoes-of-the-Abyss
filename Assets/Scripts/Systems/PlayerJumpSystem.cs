using Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        private EcsFilter<Player, EntityInfo, JumpInfo, GroundChecker, JumpEvent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref EntityInfo entityInfo = ref filter.Get2(i);
                ref JumpInfo JumpInfo = ref filter.Get3(i);
                ref GroundChecker groundChecker = ref filter.Get4(i);

                if (!groundChecker.IsGrounded) continue;

                Debug.Log($"PlayerJumpSystem: jumped");
                entityInfo.PlayerRigidbody.linearVelocityY = 0;
                entityInfo.PlayerRigidbody.AddForce(Vector2.up * JumpInfo.JumpForce, ForceMode2D.Impulse);
            }
        }
    }
}