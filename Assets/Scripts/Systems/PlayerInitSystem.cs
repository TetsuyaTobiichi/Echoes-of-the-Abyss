using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
        private PlayerData staticData;
        private SceneData sceneData;

        public void Init()
        {
            EcsEntity playerEntity = ecsWorld.NewEntity();

            ref var player = ref playerEntity.Get<Player>();
            ref var inputData = ref playerEntity.Get<PlayerInputData>();

            GameObject playerGO = Object.Instantiate(staticData.PlayerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
            player.PlayerRigidbody = playerGO.GetComponent<Rigidbody2D>();
            player.PlayerMoveSpeed = staticData.PlayerMoveSpeed;
            player.PlayerJumpForce = staticData.PlayerJumpForce;
            player.AttackSettings = staticData.AttackSettings;
        }
    }
}