﻿using System;
using Gameplay.SnakeLogic;
using Infrastructure;
using Network.Extensions;
using Services;
using UnityEngine;

namespace Network.Services
{
    public class NetworkGameFactory
    {
        private const string PlayerSnakePath = "Snake/Player Snake";
        private const string RemotePlayerSnakePath = "Snake/Remote Snake";
        private const string DetailPath = "Snake/Body Detail";
        
        private readonly INetworkStatusProvider _networkStatus;
        private readonly Assets _assets;
        private readonly CameraProvider _cameraProvider;

        public NetworkGameFactory(INetworkStatusProvider networkStatus, Assets assets, 
            CameraProvider cameraProvider)
        {
            _networkStatus = networkStatus;
            _assets = assets;
            _cameraProvider = cameraProvider;
        }

        public Snake CreateSnake(string key, Player player) => 
            _networkStatus.IsPlayer(key) 
                ? CreatePlayer(key, player) 
                : CreateRemotePlayer(key, player);

        public void RemoveSnake(string key)
        {
            
        }

        private Snake CreatePlayer(string key, Player player)
        {
            var snake = CreateSnake(PlayerSnakePath, player.position.ToVector3(), player.size);
            _cameraProvider.Follow(snake.Head.transform);
            return snake;
        }

        private Snake CreateRemotePlayer(string key, Player player)
        {
            var snake = CreateSnake(RemotePlayerSnakePath, player.position.ToVector3(), player.size);
            return snake;
        }
        
        public Snake CreateSnake(string path, Vector3 position, int countOfDetails)
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