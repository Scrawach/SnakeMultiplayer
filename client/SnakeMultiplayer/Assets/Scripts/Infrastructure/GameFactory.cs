using Gameplay.SnakeLogic;
using UnityEngine;

namespace Infrastructure
{
    public class GameFactory
    {
        private const string SnakeHeadPath = "Snake/Snake";
        private const string DetailPath = "Snake/Body Detail";

        private readonly Assets _assets;

        public GameFactory(Assets assets) => 
            _assets = assets;

        public Snake CreateSnake(Vector3 position, int countOfDetails)
        {
            var instance = _assets.Instantiate<Snake>(SnakeHeadPath, position, Quaternion.identity, null);

            for (var i = 0; i < countOfDetails; i++)
            {
                var detail = _assets.Instantiate<GameObject>(DetailPath, Vector3.zero, Quaternion.identity, instance.transform);
                instance.AddDetail(detail);
            }
            
            return instance;
        }
    }
}