using UnityEngine;

namespace Services
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _offsetY = 16f;
        
        private Transform _target;
        
        public void Follow(Transform target) => 
            _target = target;

        private void LateUpdate()
        {
            var desiredPosition = _target.transform.position;
            desiredPosition.y = _offsetY;
            transform.position = desiredPosition;
        }
    }
}