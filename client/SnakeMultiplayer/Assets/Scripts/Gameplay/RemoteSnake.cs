using Gameplay.SnakeLogic;
using Network.Extensions;
using Network.Schemas;
using UnityEngine;

namespace Gameplay
{
    public class RemoteSnake : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        
        public void ChangePosition(Vector2Schema current, Vector2Schema previous) => 
            _snake.LookAt(current.ToVector3());
    }
}