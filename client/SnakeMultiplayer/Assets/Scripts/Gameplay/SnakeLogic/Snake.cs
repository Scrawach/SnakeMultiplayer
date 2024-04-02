using Reflex.Attributes;
using Services;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.SnakeLogic
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private SnakeHead _head;
        [SerializeField] private SnakeBody _body;

        [SerializeField] private GameObject _detailPrefab;
        [SerializeField] private int _countOfDetails;

        [Inject]
        public void Construct(CameraProvider cameraProvider) => 
            cameraProvider.Follow(_head.transform);

        private void Start()
        {
            var headTransform = _head.transform;
            for (var i = 0; i < _countOfDetails; i++)
            {
                var instance = Instantiate(_detailPrefab, headTransform.position, headTransform.rotation, transform);
                _body.AddDetail(instance);
            }
        }

        public void LookAt(Vector3 target) => 
            _head.LookAt(target);

        public void ResetRotation() => 
            _head.ResetRotation();
    }
    
}