

using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class BlockMoveSystem : IEcsRunSystem
    {
        private EcsFilter<BlockMove> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref BlockMove blockInfo = ref filter.Get1(i);
                blockInfo.Time -= Time.deltaTime;

                if (blockInfo.Time <= 0)
                {
                    filter.GetEntity(i).Del<BlockMove>();
                }
            }
        }
    }
}