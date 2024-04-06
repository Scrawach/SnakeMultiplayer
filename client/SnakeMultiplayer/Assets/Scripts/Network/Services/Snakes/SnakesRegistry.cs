using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.SnakeLogic;
using Network.Schemas;

namespace Network.Services.Snakes
{
    public class SnakesRegistry
    {
        private readonly Dictionary<string, SnakeInfo> _snakes;

        public SnakesRegistry() => 
            _snakes = new Dictionary<string, SnakeInfo>();

        public SnakeInfo this[string key] => _snakes[key];

        public event Action<string> Added;
        public event Action<string> Removed;
        public event Action Updated;

        public IEnumerable<(string, SnakeInfo)> All() => 
            _snakes.Select(pair => (pair.Key, pair.Value));

        public void Add(string key, PlayerSchema player, Snake snake)
        {
            _snakes[key] = new SnakeInfo() { Snake = snake, Player = player };
            Updated?.Invoke();
            Added?.Invoke(key);
        }

        public bool Remove(string key)
        {
            var result = _snakes.Remove(key);

            if (result)
            {
                Updated?.Invoke();
                Removed?.Invoke(key);
            }
            
            return result;
        }

        public bool Contains(string key) => 
            _snakes.ContainsKey(key);
    }
}