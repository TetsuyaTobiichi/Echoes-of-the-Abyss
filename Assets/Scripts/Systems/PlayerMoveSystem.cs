using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Player, EntityInfo, MoveInfo>.Exclude<BlockMove> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref Player player = ref filter.Get1(i);
                ref EntityInfo entityInfo = ref filter.Get2(i);
                ref MoveInfo moveInfo = ref filter.Get3(i);

                entityInfo.PlayerRigidbody.linearVelocityX = moveInfo.DirectionX * moveInfo.MoveSpeed;
            }
        }
    }
}