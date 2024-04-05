using System.Linq;
using Gameplay;
using Gameplay.SnakeLogic;
using Infrastructure;
using Network.Extensions;
using Network.Schemas;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Network.Services.Factory
{
    public class NetworkGameFactory
    {
        private const string PlayerSnakePath = "Snake/Player Snake";
        private const string RemotePlayerSnakePath = "Snake/Remote Snake";
        private const string DetailPath = "Snake/Body Detail";
        
        private readonly INetworkStatusProvider _networkStatus;
        private readonly RemoteSnakesProvider _remoteSnakes;
        private readonly Assets _assets;
        private readonly CameraProvider _camera;
        private readonly StaticDataService _staticData;
        private readonly NetworkTransmitter _transmitter;
        private readonly VfxFactory _vfxFactory;

        public NetworkGameFactory(INetworkStatusProvider networkStatus, Assets assets, CameraProvider camera, 
            StaticDataService staticData, NetworkTransmitter transmitter, RemoteSnakesProvider remoteSnakes,
            VfxFactory vfxFactory)
        {
            _networkStatus = networkStatus;
            _assets = assets;
            _camera = camera;
            _staticData = staticData;
            _transmitter = transmitter;
            _remoteSnakes = remoteSnakes;
            _vfxFactory = vfxFactory;
        }

        public void RemoveSnake(string key)
        {
            if (!_remoteSnakes.Contains(key))
                return;
            
            var info = _remoteSnakes[key];
            _remoteSnakes.Remove(key);
            
            foreach (var dispose in info.Disposes) 
                dispose?.Invoke();

            var positions = info.Snake.GetBodyDetailPositions().ToArray();
            var skin = _staticData.ForSnakeSkin(info.Player.skinId);
            foreach (var position in positions) 
                _vfxFactory.CreateDeathVfx(position, skin);

            _transmitter.SendDeathSnakeDetailPositions(key, positions);
            Object.Destroy(info.Snake.gameObject);
        }
        
        public Snake CreateSnake(string key, PlayerSchema player) => 
            _networkStatus.IsPlayer(key) 
                ? CreatePlayerSnake(key, player) 
                : CreateRemoteSnake(key, player, RemotePlayerSnakePath);

        private Snake CreatePlayerSnake(string key, PlayerSchema schema)
        {
            var data = _staticData.ForSnake();
            var remoteSnake = CreateRemoteSnake(key, schema, PlayerSnakePath);
            remoteSnake.GetComponentInChildren<PlayerAim>().Construct(data.MovementSpeed, data.RotationSpeed);
            _camera.Follow(remoteSnake.Head.transform);
            return remoteSnake;
        }
        
        private Snake CreateRemoteSnake(string key, PlayerSchema schema, string pathToPrefab)
        {
            var data = _staticData.ForSnake();
            var skin = _staticData.ForSnakeSkin(schema.skinId);
            var snake = CreateSnake(pathToPrefab, schema.position.ToVector3(), skin, data.MovementSpeed);
            AddSnakeDetail(key, schema.size);

            var remoteSnake = snake.GetComponent<RemoteSnake>();
            remoteSnake.SetUsername(schema.username);
            remoteSnake.GetComponent<UniqueId>().Construct(key);
            remoteSnake.GetComponent<LeaderboardSnake>().Initialize(schema);
            
            var positionDispose = schema.OnPositionChange(remoteSnake.ChangePosition);
            var sizeChanges = schema.OnSizeChange(remoteSnake.ChangeSize);
            _remoteSnakes.Add(key, schema, snake, positionDispose, sizeChanges);

            return snake;
        }
        
        private Snake CreateSnake(string pathToPrefab, Vector3 position, Material skin, float movementSpeed)
        {
            var snake = _assets.Instantiate<Snake>(pathToPrefab, position, Quaternion.identity, null);
            snake.Head.Construct(movementSpeed);
            snake.GetComponentInChildren<SnakeSkin>().ChangeTo(skin);
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

        private GameObject CreateSnakeDetail(Transform head, Transform parent, Material skin)
        {
            var spawnPoint = head.position - head.forward;
            var instance = _assets.Instantiate<SnakeSkin>(DetailPath, spawnPoint, head.rotation, parent);
            instance.ChangeTo(skin);
            return instance.gameObject;
        }
    }
}