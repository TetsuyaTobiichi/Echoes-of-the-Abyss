using Components;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerBlockAttackSystem : IEcsRunSystem
{
    private EcsFilter<BlockAttack> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref BlockAttack block = ref filter.Get1(i);

            block.Timer -= Time.deltaTime;

            if (block.Timer <= 0)
            {
                filter.GetEntity(i).Del<BlockAttack>();
            }
        }
    }
}
