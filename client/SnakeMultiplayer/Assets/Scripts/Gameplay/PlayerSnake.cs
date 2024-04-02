using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay
{
    public class PlayerSnake : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        
        private InputService _input;

        [Inject]
        public void Construct(InputService input) => 
            _input = input;

        private void Update()
        {
            if (_input.IsMoveButtonPressed())
                _snake.LookAt(_input.WorldMousePosition());
            else
                _snake.ResetRotation();
        }
    }
}