using CameraLogic;
using UnityEngine;

namespace Services
{
    public class CameraProvider
    {
        public Camera Current { get; }

        private readonly CameraFollow _cameraFollow;

        public CameraProvider()
        {
            Current = Camera.main;
            _cameraFollow = Current.GetComponent<CameraFollow>();
        }

        public void Follow(Transform target) => 
            _cameraFollow.Follow(target);
    }
}