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

        private SnakesFactory _snakesFactory;
        
        [Inject]
        public void Construct(SnakesFactory snakesFactory) => 
            _snakesFactory = snakesFactory;

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
                _snakesFactory.AddSnakeDetail(_uniqueId.Value, -difference);
            else
                _snakesFactory.RemoveSnakeDetails(_uniqueId.Value, difference);
        }
    }
}