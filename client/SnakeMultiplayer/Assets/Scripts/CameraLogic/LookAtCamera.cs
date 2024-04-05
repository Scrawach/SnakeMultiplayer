using Reflex.Attributes;
using Services;
using UnityEngine;

namespace CameraLogic
{
    public class LookAtCamera : MonoBehaviour
    {
        private CameraProvider _cameraProvider;
        
        [Inject]
        public void Construct(CameraProvider cameraProvider) => 
            _cameraProvider = cameraProvider;

        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(_cameraProvider.Current.transform.forward, Vector3.up);
        }
    }
}