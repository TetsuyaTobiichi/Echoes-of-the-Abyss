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

#if UNITY_EDITOR
            Vector2 checkPos = entityInfo.PlayerRigidbody.transform.TransformPoint(groundChecker.CheckerPosition);
            float checkDistance = 0.2f;

            Debug.DrawRay(checkPos, Vector2.down * checkDistance, Color.red, 0.02f);
#endif

            if (Physics2D.Raycast(
                entityInfo.PlayerRigidbody.transform.TransformPoint(groundChecker.CheckerPosition),
                Vector2.down, checkDistance, mask))
            {
                groundChecker.IsGrounded = true;
            }
            else
            {
                groundChecker.IsGrounded = false;
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
