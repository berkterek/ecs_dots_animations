using EcsDotsAnimations.Components;
using EcsDotsAnimations.Inputs;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EcsDotsScripts.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup),OrderLast = true)]
    public partial class InputSystem : SystemBase
    {
        GameInputActions _input;
        float3 _moveInput;

        protected override void OnCreate()
        {
            RequireForUpdate<InputData>();
            _input = new GameInputActions();
        }

        protected override void OnStartRunning()
        {
            _input.Enable();
            _input.Player.Move.performed += HandleOnMovement;
            _input.Player.Move.canceled += HandleOnMovement;
        }

        protected override void OnStopRunning()
        {
            _input.Disable();
            _input.Player.Move.performed -= HandleOnMovement;
            _input.Player.Move.canceled -= HandleOnMovement;
        }

        protected override void OnUpdate()
        {
            var inputData = SystemAPI.GetSingleton<InputData>();
            inputData.MoveInput = _moveInput;

            SystemAPI.SetSingleton(inputData);
        }
        
        void HandleOnMovement(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            _moveInput = new float3(inputValue.x, 0f, inputValue.y);
        }
    }    
}