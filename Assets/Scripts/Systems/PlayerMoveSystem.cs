using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Player, PlayerInputData> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref Player player = ref filter.Get1(i);
                ref PlayerInputData input = ref filter.Get2(i);

                player.PlayerRigidbody.linearVelocityX = input.Direction.x * player.PlayerMoveSpeed;
            }
        }
    }
}