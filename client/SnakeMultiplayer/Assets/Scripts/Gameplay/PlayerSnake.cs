using System;
using Gameplay.SnakeLogic;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay
{
    public class PlayerSnake : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        
        private InputService _input;
        
        public Vector3 TargetPoint { get; private set; }
        
        [Inject]
        public void Construct(InputService input) => 
            _input = input;

        private void Update()
        {
            if (_input.IsMoveButtonPressed())
            {
                TargetPoint = _input.WorldMousePosition();
                _snake.SmoothLookAt(TargetPoint);
            }
            else
            {
                _snake.ResetRotation();
            }
        }
    }
}