using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class LookSystem : IEcsRunSystem
    {
        private EcsFilter<EntityInfo, LookParams, MoveInfo> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref EntityInfo entityInfo = ref filter.Get1(i);
                ref LookParams lookParams = ref filter.Get2(i);
                ref MoveInfo moveInfo = ref filter.Get3(i);

                if (moveInfo.DirectionX != 0)
                {
                    lookParams.LookDirection = moveInfo.DirectionX > 0 ? Vector2.right : Vector2.left;
                    entityInfo.PlayerRigidbody.transform.rotation = moveInfo.DirectionX > 0 ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
                }
            }
        }
    }
}