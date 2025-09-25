using Components;
using Leopotam.Ecs;
using UnityEngine;

/// <summary>
/// TODO: makeWallSlideCheckSystem N wallSlideJumpSystem
/// </summary>
public class PlayerWallSlideingSystem : IEcsRunSystem
{
    private EcsFilter<Player, EntityInfo, LookParams, WallSlide, GroundChecker> filter;
    private static int mask = ~LayerMask.GetMask("Player", "Enemy");
    public void Run()
    {
        foreach (var i in filter)
        {
            ref Player player = ref filter.Get1(i);
            ref EntityInfo entityInfo = ref filter.Get2(i);
            ref LookParams lookParams = ref filter.Get3(i);
            ref WallSlide wallSlideParam = ref filter.Get4(i);
            ref GroundChecker groundChecker = ref filter.Get5(i);

            Vector2 checkPos = entityInfo.PlayerRigidbody.transform.TransformPoint(new Vector3(0, 0.75f, 0));
#if UNITY_EDITOR
            Debug.DrawRay(entityInfo.PlayerRigidbody.transform.TransformPoint(new Vector3(0, 0.75f, 0)), lookParams.LookDirection * 0.3f, Color.red, 0.001f);
#endif
            if (!groundChecker.IsGrounded &&
            Physics2D.Raycast(checkPos, lookParams.LookDirection, 0.3f, mask))
            {
                wallSlideParam.IsSliding = true;

                if (entityInfo.PlayerRigidbody.linearVelocityY < -wallSlideParam.SlideMaxSpeed)
                {
                    entityInfo.PlayerRigidbody.linearVelocityY = -wallSlideParam.SlideMaxSpeed;
                }
            }
            else
            {
                wallSlideParam.IsSliding = false;
            }

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
