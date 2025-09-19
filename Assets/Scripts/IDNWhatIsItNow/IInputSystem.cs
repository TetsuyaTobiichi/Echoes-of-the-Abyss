using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public interface IInputSystem
    {
        public InputSystem_Actions Input { get; }
    }
}