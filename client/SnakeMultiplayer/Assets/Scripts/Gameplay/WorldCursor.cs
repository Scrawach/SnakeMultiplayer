using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay
{
    public class WorldCursor : MonoBehaviour
    {
        private InputService _input;
    
        [Inject]
        public void Construct(InputService input) => 
            _input = input;

        private void Update()
        {
            if (_input.IsMoveButtonPressed()) 
                transform.position = _input.WorldMousePosition();
        }
    }
}