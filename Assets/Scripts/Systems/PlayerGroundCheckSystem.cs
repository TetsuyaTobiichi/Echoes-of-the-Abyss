using Components;
using Leopotam.Ecs;
using UnityEngine;

/// <summary>
/// TODO: make system under all player width
/// </summary>
public class PlayerGroundCheckSystem : IEcsRunSystem
{
    private EcsFilter<Player, GroundChecker, EntityInfo> filter;
    private static int mask = ~LayerMask.GetMask("Player", "Enemy");

    public void Run()
    {
        foreach (var i in filter)
        {
            ref GroundChecker groundChecker = ref filter.Get2(i);
            ref EntityInfo entityInfo = ref filter.Get3(i);

            Vector2 checkPos = entityInfo.PlayerRigidbody.transform.TransformPoint(groundChecker.CheckerPosition);
            float checkDistance = 0.2f;

#if UNITY_EDITOR
            Debug.DrawRay(checkPos, Vector2.down * checkDistance, Color.red, 0.02f);
#endif

            groundChecker.IsGrounded = Physics2D.Raycast(checkPos, Vector2.down, checkDistance, mask);
        }
    }
}
