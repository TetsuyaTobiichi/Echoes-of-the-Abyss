using System;
using Components.Container;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Systems
{
    public class BasePlayerInputSystem : MonoBehaviour, IInputSystem
    {
        public InputSystem_Actions Input => _input;
        private InputSystem_Actions _input;

        protected InputAction Move => _input.Player.Move;
        protected InputAction Jump => _input.Player.Jump;

        protected InputAction Attack => _input.Player.Attack;
        protected InputAction Interact => _input.Player.Interact;

        public virtual void Initialize(IObjectsContainer container)
        {
            _input = new InputSystem_Actions();
            _input.Enable();
        }

        public void EnableControll()
        {
            _input.Enable();
        }

        public void DisableControll()
        {
            _input.Disable();
        }
    }
}