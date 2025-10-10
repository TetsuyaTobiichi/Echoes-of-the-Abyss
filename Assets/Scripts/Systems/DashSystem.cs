using Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    /// <summary>
    /// TODO: addBlockDash system
    /// </summary>
    public class DashSystem : IEcsRunSystem
    {
        private EcsFilter<EntityInfo, LookParams, Dash, DashEvent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref EntityInfo entityInfo = ref filter.Get1(i);
                ref LookParams lookParams = ref filter.Get2(i);
                ref Dash dashInfo = ref filter.Get3(i);


                ref var block = ref filter.GetEntity(i).Get<BlockMove>();
                entityInfo.PlayerRigidbody.AddForce(lookParams.LookDirection * dashInfo.DashForce, ForceMode2D.Impulse);

                Debug.Log($"DashSystem: dash direction {(lookParams.LookDirection + Vector2.up)} " + lookParams.LookDirection + "  " + dashInfo.DashForce);
                block.Time = 0.2f;
            }
        }
    }
}