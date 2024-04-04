using System;
using System.Collections.Generic;
using Gameplay.SnakeLogic;
using Network.Schemas;

namespace Network.Services.Snakes
{
    public class RemoteSnakesProvider
    {
        private readonly Dictionary<string, RemoteSnakeInfo> _snakes;

        public RemoteSnakesProvider() => 
            _snakes = new Dictionary<string, RemoteSnakeInfo>();

        public RemoteSnakeInfo this[string key] => _snakes[key];
        
        public void Add(string key, PlayerSchema player, Snake snake, params Action[] disposes) => 
            _snakes[key] = new RemoteSnakeInfo() { Snake = snake, Player = player, Disposes = disposes };

        public bool Remove(string key) => 
            _snakes.Remove(key);

        public bool Contains(string key) => 
            _snakes.ContainsKey(key);
    }
}