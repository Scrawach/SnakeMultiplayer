using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _offsetY = 16f;
        
        private Transform _target;
        
        public void Follow(Transform target) => 
            _target = target;

        private void LateUpdate()
        {
            if (_target == null)
                return;
            
            var desiredPosition = _target.transform.position;
            desiredPosition.y = _offsetY;
            transform.position = desiredPosition;
        }
    }
}