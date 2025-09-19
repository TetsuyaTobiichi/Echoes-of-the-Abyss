using Components;
using Components.Container;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private EcsFilter<Player, PlayerInputData>.Exclude<BlockAttack> filter;
        private IObjectsContainer container;
        private static int mask = ~LayerMask.GetMask("Player");
        public void Run()
        {
            foreach (var i in filter)
            {
                ref Player player = ref filter.Get1(i);
                ref PlayerInputData input = ref filter.Get2(i);

                if (input.IsAttacked)
                {
                    Vector2 checkPos = player.PlayerRigidbody.transform.TransformPoint(new Vector3(0, 0.75f, 0));
#if UNITY_EDITOR
                    Debug.DrawRay(checkPos, Vector2.right * player.AttackSettings.AttackRange, Color.green, 0.001f);
#endif

                    RaycastHit2D[] AttakedEnemyes = Physics2D.RaycastAll(checkPos, Vector2.right, player.AttackSettings.AttackRange, mask);
                    foreach (var enemy in AttakedEnemyes)
                    {
                        if (enemy.transform.name == "player(clone)") continue;
                        Debug.Log("transform " + enemy.transform?.name);
                    }
                    
                    filter.GetEntity(i).Get<BlockAttack>().Timer = 1f / player.AttackSettings.AttackSpeed;

                    input.IsAttacked = false;
                }
            }
        }
    }
}