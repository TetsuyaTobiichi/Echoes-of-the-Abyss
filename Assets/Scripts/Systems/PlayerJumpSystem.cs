using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        private EcsFilter<Player, EntityInfo, JumpInfo> filter;
        //position under player prefab, for check ground collision
        private static readonly Vector3 _groundCheckerPosition = new Vector3(0, 0.265f, 0);
        private static int mask = ~LayerMask.GetMask("Player", "Enemy");

        public void Run()
        {
            foreach (var i in filter)
            {
                ref Player player = ref filter.Get1(i);
                ref EntityInfo entityInfo = ref filter.Get2(i);
                ref JumpInfo JumpInfo = ref filter.Get3(i);

#if UNITY_EDITOR
                Vector2 checkPos = entityInfo.PlayerRigidbody.transform.TransformPoint(_groundCheckerPosition);
                float checkDistance = 0.2f;

                Debug.DrawRay(checkPos, Vector2.down * checkDistance, Color.red, 0.02f);

                RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, checkDistance);
#endif  

                if (JumpInfo.IsJumping)
                {
                    if (Physics2D.Raycast(
                        entityInfo.PlayerRigidbody.transform.TransformPoint(_groundCheckerPosition),
                        Vector2.down, 0.2f, mask))
                    {
                        Debug.Log($"PlayerJumpSystem: jumped");
                        entityInfo.PlayerRigidbody.linearVelocityY = 0;
                        entityInfo.PlayerRigidbody.AddForce(Vector2.up * JumpInfo.JumpForce, ForceMode2D.Impulse);
                    }
                    JumpInfo.IsJumping = false;
                }
            }
        }
    }
}