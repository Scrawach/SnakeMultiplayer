using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class SnakeHead : MonoBehaviour
    {
        private float _speed;
        private Quaternion _targetRotation;

        public void Construct(float speed) => 
            _speed = speed;

        public void LookAt(Vector3 target)
        {
            var direction = target - transform.position;
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = _targetRotation;
        }

        public void ResetRotation() => 
            _targetRotation = transform.rotation;

        private void Update()
        {
            MoveHead();
        }

        private void MoveHead()
        {
            var timeStep = Time.deltaTime * _speed;
            transform.Translate(transform.forward * timeStep, Space.World);
        }
    }
}