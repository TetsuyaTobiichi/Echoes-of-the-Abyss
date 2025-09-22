using Components;
using Leopotam.Ecs;
using Mono.Cecil.Cil;
using UnityEngine;

namespace Systems
{
    public class DashSystem : IEcsRunSystem
    {
        private EcsFilter<EntityInfo, LookParams, Dash> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref EntityInfo entityInfo = ref filter.Get1(i);
                ref LookParams lookParams = ref filter.Get2(i);
                ref Dash dashInfo = ref filter.Get3(i);

                if (dashInfo.IsDashing)
                {
                    ref var block = ref filter.GetEntity(i).Get<BlockMove>();
                    entityInfo.PlayerRigidbody.AddForce(lookParams.LookDirection * dashInfo.DashForce, ForceMode2D.Impulse);
                    dashInfo.IsDashing = false;
                    Debug.Log($"here+{(lookParams.LookDirection + Vector2.up)} " + lookParams.LookDirection + "  " + dashInfo.DashForce);
                    block.Time = 0.2f;
                }
            }
        }
    }
}