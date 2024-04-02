using UnityEngine;

namespace Services
{
    public class CameraProvider
    {
        public Camera Current { get; }

        public CameraProvider() => 
            Current = Camera.main;

        public void Follow(Transform target) => 
            Current.GetComponent<CameraFollow>().Follow(target);
    }
}