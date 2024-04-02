using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameFactory _gameFactory;
        private CameraProvider _cameraProvider;
        
        [Inject]
        public void Construct(GameFactory factory, CameraProvider cameraProvider)
        {
            _gameFactory = factory;
            _cameraProvider = cameraProvider;
        }

        private void Start()
        {
            var snake = _gameFactory.CreateSnake(Vector3.zero, 9);
            _cameraProvider.Follow(snake.Head.transform);
        }
    }
}