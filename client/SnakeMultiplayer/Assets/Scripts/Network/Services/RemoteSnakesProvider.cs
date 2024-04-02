using System;
using System.Collections.Generic;
using Gameplay.SnakeLogic;

namespace Network.Services
{
    public class RemoteSnakesProvider
    {
        private readonly Dictionary<string, RemoteSnakeInfo> _snakes;

        public RemoteSnakesProvider() => 
            _snakes = new Dictionary<string, RemoteSnakeInfo>();

        public RemoteSnakeInfo this[string key] => _snakes[key];
        
        public void Add(string key, Snake snake, params Action[] disposes) => 
            _snakes[key] = new RemoteSnakeInfo() { Snake = snake, Disposes = disposes };

        public bool Remove(string key) => 
            _snakes.Remove(key);
    }
}