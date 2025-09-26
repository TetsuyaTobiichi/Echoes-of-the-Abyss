using Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
namespace Systems
{
    public class PlayerInputSystem : IEcsInitSystem
    {
        private EcsFilter<Player, PlayerInputData> _filter;
        private IInputSystem _inputSystem;

        private EcsEntity _player;
        public void Init()
        {
            var input = new InputSystem();

            input.Initialize();

            _inputSystem = input;

            SetInputData();
        }

        private void SetInputData()
        {
            foreach (var i in _filter)
            {
                int idx = i;
                _player = _filter.GetEntity(idx);
                _inputSystem.Input.Player.Move.performed += (cbc) => OnMovePerformed(cbc, idx);
                _inputSystem.Input.Player.Move.canceled += (cbc) => OnMoveCanceled(cbc, idx);
                _inputSystem.Input.Player.Jump.performed += (cbc) => OnJumpPerformed(cbc, idx);
                _inputSystem.Input.Player.Attack.performed += (cbc) => OnAttackPerforemed(cbc, idx);
                _inputSystem.Input.Player.Sprint.performed += (cbc) => OnDashPerformed(cbc, idx);
            }
        }

        private void OnMovePerformed(CallbackContext context, int idx)
        {
            ref var inputRef = ref _player.Get<MoveInfo>();
            inputRef.DirectionX = context.ReadValue<Vector2>().x;
        }

        private void OnMoveCanceled(CallbackContext context, int idx)
        {
            ref var inputRef = ref _player.Get<MoveInfo>();
            inputRef.DirectionX = 0;
        }

        private void OnJumpPerformed(CallbackContext context, int idx)
        {
            _player.Get<JumpEvent>();
        }
        /// <summary>
        /// TODO: remake on AttackEvent n dashEvent
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idx"></param>
        private void OnAttackPerforemed(CallbackContext context, int idx)
        {
            ref var inputRef = ref _player.Get<PlayerInputData>();
            inputRef.IsAttacked = true;
        }

        private void OnDashPerformed(CallbackContext context, int idx)
        {
            ref var dashInfo = ref _player.Get<Dash>();
            dashInfo.IsDashing = true;
        }

    }
}