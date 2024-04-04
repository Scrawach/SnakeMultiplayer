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

        private const string ApplePath = "Apple/Apple";
        
        private readonly INetworkStatusProvider _networkStatus;
        private readonly Assets _assets;
        private readonly CameraProvider _camera;
        private readonly RemoteSnakesProvider _remoteSnakes;
        private readonly StaticDataService _staticData;
        private readonly NetworkTransmitter _transmitter;

        public NetworkGameFactory(INetworkStatusProvider networkStatus, Assets assets, CameraProvider camera,
            RemoteSnakesProvider remoteSnakes, StaticDataService staticData, NetworkTransmitter transmitter)
        {
            _networkStatus = networkStatus;
            _assets = assets;
            _camera = camera;
            _remoteSnakes = remoteSnakes;
            _staticData = staticData;
            _transmitter = transmitter;
        }

        public Snake CreateSnake(string key, PlayerSchema player) => 
            _networkStatus.IsPlayer(key) 
                ? CreatePlayer(key, player) 
                : CreateRemotePlayer(key, player);

        public void RemoveSnake(string key)
        {
            var info = _remoteSnakes[key];
            _remoteSnakes.Remove(key);
            
            foreach (var dispose in info.Disposes) 
                dispose?.Invoke();
            
            _transmitter.SendDeathSnakeDetailPositions(key, info.Snake.Body.GetBodyDetailPositions());
            Object.Destroy(info.Snake.gameObject);
        }

        private Snake CreatePlayer(string key, PlayerSchema player)
        {
            var data = _staticData.ForSnake();
            var snake = CreateRemoteSnake(key, PlayerSnakePath, player, data.MovementSpeed);
            snake.GetComponentInChildren<PlayerAim>().Construct(data.MovementSpeed, data.RotationSpeed);
            _camera.Follow(snake.Head.transform);
            return snake;
        }

        private Snake CreateRemotePlayer(string key, PlayerSchema player)
        {
            var data = _staticData.ForSnake();
            return CreateRemoteSnake(key, RemotePlayerSnakePath, player, data.MovementSpeed);
        }

        private Snake CreateRemoteSnake(string key, string pathToPrefab, PlayerSchema player, float movementSpeed)
        {
            var skin = _staticData.ForSnakeSkin(player.skinId);
            var snake = CreateSnake(pathToPrefab, player.position.ToVector3(), player.size, skin);
            var remoteSnake = snake.GetComponent<RemoteSnake>();
            snake.GetComponent<UniqueId>().Construct(key);
            var positionDispose = player.OnPositionChange(remoteSnake.ChangePosition);
            var sizeChanges = player.OnSizeChange(remoteSnake.ChangeSize);
            _remoteSnakes.Add(key, player,snake, positionDispose, sizeChanges);
            snake.Head.Construct(movementSpeed);
            return snake;
        }

        public void AddSnakeDetail(string snakeId, int count)
        {
            var snakeInfo = _remoteSnakes[snakeId];
            var skin = _staticData.ForSnakeSkin(snakeInfo.Player.skinId);
            for (var i = 0; i < count; i++)
                snakeInfo.Snake.AddDetail(CreateSnakeDetail(snakeInfo.Snake.Head.transform, snakeInfo.Snake.transform, skin));
        }

        public void RemoveSnakeDetails(string snakeId, int count)
        {
            var snakeInfo = _remoteSnakes[snakeId];
            for (var i = 0; i < count; i++) 
                Object.Destroy(snakeInfo.Snake.RemoveDetail());
        }

        private Snake CreateSnake(string path, Vector3 position, int countOfDetails, Material skin)
        {
            var instance = _assets.Instantiate<Snake>(path, position, Quaternion.identity, null);
            instance.GetComponentInChildren<SnakeSkin>().ChangeTo(skin);
            
            for (var i = 0; i < countOfDetails; i++) 
                instance.AddDetail(CreateSnakeDetail(instance.Head.transform, instance.transform, skin));

            return instance;
        }

        private GameObject CreateSnakeDetail(Transform head, Transform parent, Material skin)
        {
            var instance = _assets.Instantiate<SnakeSkin>(DetailPath, head.position - head.forward, head.rotation, parent);
            instance.ChangeTo(skin);
            return instance.gameObject;
        }

        public Apple CreateApple(string key, AppleSchema schema)
        {
            var apple = _assets.Instantiate<Apple>(ApplePath, schema.position.ToVector3(), Quaternion.identity, null);
            apple.GetComponent<UniqueId>().Construct(key);
            schema.OnPositionChange(apple.ChangePosition);
            return apple;
        }

        public void RemoveApple(string key)
        {
            
        }
    }
}