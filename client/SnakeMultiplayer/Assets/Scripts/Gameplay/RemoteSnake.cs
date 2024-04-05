using Gameplay.SnakeLogic;
using Network.Extensions;
using Network.Schemas;
using Network.Services.Factory;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class RemoteSnake : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private UniqueId _uniqueId;
        [SerializeField] private TextMeshProUGUI _usernameLabel;

        private NetworkGameFactory _gameFactory;
        
        [Inject]
        public void Construct(NetworkGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void SetUsername(string username) => 
            _usernameLabel.text = username;


        public void ChangePosition(Vector2Schema current, Vector2Schema previous) => 
            _snake.LookAt(current.ToVector3());

        public void ChangeSize(byte current, byte previous)
        {
            if (_snake.Body.Size == current)
                return;

            ProcessChangeSizeTo(current);
        }

        private void ProcessChangeSizeTo(int target)
        {
            var difference = _snake.Body.Size - target;

            if (difference < 0)
                _gameFactory.AddSnakeDetail(_uniqueId.Value, -difference);
            else
                _gameFactory.RemoveSnakeDetails(_uniqueId.Value, difference);
        }
    }
}