using UnityEngine;

namespace Gameplay
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _rotationSpeed = 180f;

        private Quaternion _targetRotation;
        
        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            var timeStep = Time.deltaTime * _speed;
            transform.Translate(transform.forward * timeStep, Space.World);
        }

        private void Rotate()
        {
            var timeStep = Time.deltaTime * _rotationSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, timeStep);
        }

        public void LookAt(Vector3 target)
        {
            var direction = target - transform.position;
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        public void ResetRotation() => 
            _targetRotation = transform.rotation;
    }
}