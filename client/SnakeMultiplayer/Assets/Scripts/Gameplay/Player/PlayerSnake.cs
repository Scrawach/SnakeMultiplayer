using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerSnake : MonoBehaviour
    {
        [SerializeField] private PlayerAim _playerAim;
        
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
                _playerAim.SmoothLookAt(TargetPoint);
            }
            else
            {
                _playerAim.ResetRotation();
            }
        }
    }
}