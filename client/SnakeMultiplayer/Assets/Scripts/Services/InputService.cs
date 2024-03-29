using UnityEngine;

namespace Services
{
    public class InputService
    {
        private readonly CameraProvider _cameraProvider;
        private Plane _ground;

        public InputService(CameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _ground = new Plane(Vector3.up, Vector3.zero);
        }

        public bool IsMoveButtonPressed() => 
            Input.GetMouseButton(0);

        public Vector3 WorldMousePosition()
        {
            var rayFromMouse = _cameraProvider.Current.ScreenPointToRay(Input.mousePosition);
            _ground.Raycast(rayFromMouse, out var distance);
            return rayFromMouse.GetPoint(distance);
        }
    }
}