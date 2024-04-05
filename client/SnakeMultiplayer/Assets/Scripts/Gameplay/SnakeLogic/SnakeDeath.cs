using Gameplay.Common;
using Network.Services.Factory;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class SnakeDeath : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;
        
        private NetworkGameFactory _gameFactory;
        
        [Inject]
        public void Construct(NetworkGameFactory gameFactory) => 
            _gameFactory = gameFactory;
        
        public void Die() => 
            _gameFactory.RemoveSnake(_uniqueId.Value);
    }
}