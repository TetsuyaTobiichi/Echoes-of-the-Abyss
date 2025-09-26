using Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerWallJumpSystem : IEcsRunSystem
{
    private EcsFilter<Player, EntityInfo, WallSlide, JumpInfo, LookParams, JumpEvent> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref EntityInfo entityInfo = ref filter.Get2(i);
            ref WallSlide wallSlideParam = ref filter.Get3(i);
            ref JumpInfo jumpinfo = ref filter.Get4(i);
            ref LookParams lookParams = ref filter.Get5(i);

            if (!wallSlideParam.IsSliding) continue;

            ref var block = ref filter.GetEntity(i).Get<BlockMove>();
            entityInfo.PlayerRigidbody.linearVelocity = Vector2.zero;
            entityInfo.PlayerRigidbody.AddForce(new Vector2(-lookParams.LookDirection.x * 0.5f, 1f) * jumpinfo.JumpForce, ForceMode2D.Impulse);
            block.Time = 0.2f;
        }
    }
}
