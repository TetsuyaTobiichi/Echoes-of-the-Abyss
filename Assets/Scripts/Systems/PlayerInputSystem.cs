using Components;
using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
namespace Systems
{
    public class PlayerInputSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerInputData> _filter;
        private IInputSystem _inputSystem;
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
                _inputSystem.Input.Player.Move.performed += (cbc) => OnMovePerformed(cbc, idx);
                _inputSystem.Input.Player.Move.canceled += (cbc) => OnMoveCanceled(cbc, idx);
                _inputSystem.Input.Player.Jump.performed += (cbc) => OnJumpPerformed(cbc, idx);
                _inputSystem.Input.Player.Attack.performed += (cbc) => OnAttackPerforemed(cbc, idx);
            }
        }

        private void OnMovePerformed(CallbackContext context, int idx)
        {
            ref var inputRef = ref _filter.Get1(idx);
            inputRef.Direction = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(CallbackContext context, int idx)
        {
            ref var inputRef = ref _filter.Get1(idx);
            inputRef.Direction = Vector2.zero;
        }

        private void OnJumpPerformed(CallbackContext context, int idx)
        {
            ref var inputRef = ref _filter.Get1(idx);
            inputRef.IsJumped = true;
        }

        private void OnAttackPerforemed(CallbackContext context, int idx)
        {
            Debug.Log("here");
            ref var inputRef = ref _filter.Get1(idx);
            inputRef.IsAttacked = true;
        }

    }
}