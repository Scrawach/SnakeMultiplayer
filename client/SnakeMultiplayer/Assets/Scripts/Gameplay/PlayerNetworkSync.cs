using Gameplay.SnakeLogic;
using Network.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay
{
    public class PlayerNetworkSync : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        private NetworkTransmitter _transmitter;
        
        [Inject]
        public void Construct(NetworkTransmitter transmitter) => 
            _transmitter = transmitter;

        private void Update() => 
            _transmitter.SendPosition(_snake.Head.transform.position);
    }
}