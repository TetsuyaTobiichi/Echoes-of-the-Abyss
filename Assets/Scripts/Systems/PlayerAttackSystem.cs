using Components;
using Components.Container;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private EcsFilter<Player, PlayerInputData, LookParams, EntityInfo>.Exclude<BlockAttack> filter;
        private IObjectsContainer container;
        private static int mask = ~LayerMask.GetMask("Player");
        public void Run()
        {
            foreach (var i in filter)
            {
                ref Player player = ref filter.Get1(i);
                ref PlayerInputData input = ref filter.Get2(i);
                ref LookParams lookParams = ref filter.Get3(i);
                ref EntityInfo entityInfo = ref filter.Get4(i);
#if UNITY_EDITOR
                Debug.DrawRay(entityInfo.PlayerRigidbody.transform.TransformPoint(new Vector3(0, 0.75f, 0)), lookParams.LookDirection * player.AttackSettings.AttackRange, Color.green, 0.001f);
#endif
                if (input.IsAttacked)
                {
                    Vector2 checkPos = entityInfo.PlayerRigidbody.transform.TransformPoint(new Vector3(0, 0.75f, 0));


                    RaycastHit2D[] AttakedEnemyes = Physics2D.RaycastAll(checkPos, lookParams.LookDirection, player.AttackSettings.AttackRange, mask);
                    foreach (var enemy in AttakedEnemyes)
                    {
                        Debug.Log("transform " + enemy.transform?.name);
                    }

                    filter.GetEntity(i).Get<BlockAttack>().Timer = 1f / player.AttackSettings.AttackSpeed;

                    input.IsAttacked = false;
                }
            }
        }
    }
}