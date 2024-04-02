using Gameplay;
using Gameplay.SnakeLogic;
using Infrastructure;
using Network.Extensions;
using Network.Schemas;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Services;
using UnityEngine;

namespace Network.Services.Factory
{
    public class NetworkGameFactory
    {
        private const string PlayerSnakePath = "Snake/Player Snake";
        private const string RemotePlayerSnakePath = "Snake/Remote Snake";
        private const string DetailPath = "Snake/Body Detail";
        
        private readonly INetworkStatusProvider _networkStatus;
        private readonly Assets _assets;
        private readonly CameraProvider _camera;
        private readonly RemoteSnakesProvider _remoteSnakes;

        public NetworkGameFactory(INetworkStatusProvider networkStatus, Assets assets, CameraProvider camera,
            RemoteSnakesProvider remoteSnakes)
        {
            _networkStatus = networkStatus;
            _assets = assets;
            _camera = camera;
            _remoteSnakes = remoteSnakes;
        }

        public Snake CreateSnake(string key, Player player) => 
            _networkStatus.IsPlayer(key) 
                ? CreatePlayer(key, player) 
                : CreateRemotePlayer(key, player);

        public void RemoveSnake(string key)
        {
            var info = _remoteSnakes[key];
            _remoteSnakes.Remove(key);
            foreach (var dispose in info.Disposes) 
                dispose?.Invoke();
            Object.Destroy(info.Snake.gameObject);
        }

        private Snake CreatePlayer(string key, Player player)
        {
            var snake = CreateRemoteSnake(key, PlayerSnakePath, player);
            _camera.Follow(snake.Head.transform);
            return snake;
        }

        private Snake CreateRemotePlayer(string key, Player player) => 
            CreateRemoteSnake(key, RemotePlayerSnakePath, player);

        private Snake CreateRemoteSnake(string key, string pathToPrefab, Player player)
        {
            var snake = CreateSnake(pathToPrefab, player.position.ToVector3(), player.size);
            var remoteSnake = snake.GetComponent<RemoteSnake>();
            var positionDispose = player.OnPositionChange(remoteSnake.ChangePosition);
            _remoteSnakes.Add(key, snake, positionDispose);
            return snake;
        }

        private Snake CreateSnake(string path, Vector3 position, int countOfDetails)
        {
            var instance = _assets.Instantiate<Snake>(path, position, Quaternion.identity, null);

            for (var i = 0; i < countOfDetails; i++)
            {
                var detail = _assets.Instantiate<GameObject>(DetailPath, Vector3.zero, Quaternion.identity, instance.transform);
                instance.AddDetail(detail);
            }
            
            return instance;
        }
    }
}