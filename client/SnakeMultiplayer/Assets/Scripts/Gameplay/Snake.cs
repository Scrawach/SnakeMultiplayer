using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private MoveForward _move;
        
        private InputService _input;

        [Inject]
        public void Construct(InputService input) => 
            _input = input;

        private void Update()
        {
            if (_input.IsMoveButtonPressed())
                _move.LookAt(_input.WorldMousePosition());
            else
                _move.ResetRotation();
        }
    }
}