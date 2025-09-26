using Components;
using Leopotam.Ecs;
using UnityEngine;

/// <summary>
/// TODO: makeWallSlideCheckSystem N wallSlideJumpSystem
/// </summary>
public class PlayerWallSlideingSystem : IEcsRunSystem
{
    private EcsFilter<Player, EntityInfo, WallCheker, WallSlide, GroundChecker> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref Player player = ref filter.Get1(i);
            ref EntityInfo entityInfo = ref filter.Get2(i);
            ref WallCheker wallCheker = ref filter.Get3(i);
            ref WallSlide wallSlideParam = ref filter.Get4(i);
            ref GroundChecker groundChecker = ref filter.Get5(i);

            if (!groundChecker.IsGrounded && wallCheker.IsNearwall)
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
