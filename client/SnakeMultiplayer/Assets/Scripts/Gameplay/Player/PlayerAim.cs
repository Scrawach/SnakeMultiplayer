using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private float _speed;
        private float _rotationSpeed;
        
        private Quaternion _targetRotation;

        public void Construct(float speed, float rotationSpeed)
        {
            _speed = speed;
            _rotationSpeed = rotationSpeed;
        }

        public void SmoothLookAt(Vector3 target)
        {
            var direction = target - transform.position;
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        
        public void ResetRotation() => 
            _targetRotation = transform.rotation;

        private void Update()
        {
            MoveHead();
            RotateHead();
        }

        private void MoveHead()
        {
            var timeStep = Time.deltaTime * _speed;
            transform.Translate(transform.forward * timeStep, Space.World);
        }

        private void RotateHead()
        {
            var timeStep = Time.deltaTime * _rotationSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, timeStep);
        }
    }
}