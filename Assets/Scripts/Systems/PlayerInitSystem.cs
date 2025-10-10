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
            ref var moveInfo = ref playerEntity.Get<MoveInfo>();
            ref var JumpInfo = ref playerEntity.Get<JumpInfo>();
            ref var EntityInfo = ref playerEntity.Get<EntityInfo>();
            ref var LookParams = ref playerEntity.Get<LookParams>();
            ref var Dash = ref playerEntity.Get<Dash>();
            ref var GroundChecker = ref playerEntity.Get<GroundChecker>();
            ref var WallSlide = ref playerEntity.Get<WallSlide>();
            ref var WallChecker = ref playerEntity.Get<WallCheker>();

            GameObject playerGO = Object.Instantiate(staticData.PlayerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
            EntityInfo.PlayerRigidbody = playerGO.GetComponent<Rigidbody2D>();

            //player controll info
            ///speed
            moveInfo.MoveSpeed = staticData.PlayerMoveSpeed;
            WallSlide.SlideMaxSpeed = 0.2f;
            ///forces
            JumpInfo.JumpForce = staticData.PlayerJumpForce;
            Dash.DashForce = 5f;
            ///infos
            player.AttackSettings = staticData.AttackSettings;
            LookParams.LookDirection = Vector2.right;


            //
            //TODO: think how automate it
            GroundChecker.CheckerPosition = new Vector3(0, 0.265f, 0);
            WallChecker.CheckerPosition = new Vector3(0, 0.75f, 0);
        }
    }
}