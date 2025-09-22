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
            ref var moveInfo = ref playerEntity.Get<MoveInfo>();
            ref var JumpInfo = ref playerEntity.Get<JumpInfo>();
            ref var EntityInfo = ref playerEntity.Get<EntityInfo>();
            ref var LookParams = ref playerEntity.Get<LookParams>();
            ref var Dash = ref playerEntity.Get<Dash>();

            GameObject playerGO = Object.Instantiate(staticData.PlayerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
            EntityInfo.PlayerRigidbody = playerGO.GetComponent<Rigidbody2D>();
            moveInfo.MoveSpeed = staticData.PlayerMoveSpeed;
            JumpInfo.JumpForce = staticData.PlayerJumpForce;
            player.AttackSettings = staticData.AttackSettings;
            LookParams.LookDirection = Vector2.right;
            Dash.DashForce = 5f;
        }
    }
}