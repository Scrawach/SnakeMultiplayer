using Gameplay.SnakeLogic;
using Network.Schemas;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using UnityEngine;

namespace Network.Services.Factory
{
    public class NetworkGameFactory
    {
        private readonly INetworkStatusProvider _networkStatus;
        private readonly SnakesRegistry _snakes;
        private readonly SnakesFactory _snakesFactory;
        private readonly SnakesDestruction _snakesDestruction;

        public NetworkGameFactory(INetworkStatusProvider networkStatus, SnakesRegistry snakes, 
            SnakesFactory snakesFactory, SnakesDestruction snakesDestruction)
        {
            _networkStatus = networkStatus;
            _snakes = snakes;
            _snakesFactory = snakesFactory;
            _snakesDestruction = snakesDestruction;
        }

        public void RemoveSnake(string key)
        {
            if (!_snakes.Contains(key))
                return;
            
            _snakesDestruction.Destruct(key);
            _snakesFactory.RemoveSnake(key);
        }
        
        public Snake CreateSnake(string key, PlayerSchema player) => 
            _networkStatus.IsPlayer(key) 
                ? _snakesFactory.CreatePlayerSnake(key, player) 
                : _snakesFactory.CreateRemoteSnake(key, player);
        
    }
}