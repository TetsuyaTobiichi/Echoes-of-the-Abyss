using Components;
using Leopotam.Ecs;
using UnityEngine;

/// <summary>
/// TODO: make system under all player width
/// </summary>
public class PlayerWallCheckSystem : IEcsRunSystem
{
    private EcsFilter<Player, EntityInfo, LookParams, WallCheker> filter;
    private static int mask = ~LayerMask.GetMask("Player", "Enemy");
    public void Run()
    {
        foreach (var i in filter)
        {
            ref Player player = ref filter.Get1(i);
            ref EntityInfo entityInfo = ref filter.Get2(i);
            ref LookParams lookParams = ref filter.Get3(i);
            ref WallCheker wallCheker = ref filter.Get4(i);

            Vector2 checkPos = entityInfo.PlayerRigidbody.transform.TransformPoint(wallCheker.CheckerPosition);

#if UNITY_EDITOR
            Debug.DrawRay(checkPos, lookParams.LookDirection * 0.2f, Color.red, 0.001f);
#endif

            wallCheker.IsNearwall = Physics2D.Raycast(checkPos, lookParams.LookDirection, 0.3f, mask);
        }
    }
}
