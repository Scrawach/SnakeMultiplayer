using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.SnakeLogic
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private SnakeHead _head;
        [SerializeField] private SnakeBody _body;
        
        public SnakeHead Head => _head;

        public void AddDetail(GameObject detail) => 
            _body.AddDetail(detail);

        public void SmoothLookAt(Vector3 target) => 
            _head.LookAt(target);
        
        public void LookAt(Vector3 target) => 
            _head.LookAt(target);

        public void ResetRotation() => 
            _head.ResetRotation();
    }
}