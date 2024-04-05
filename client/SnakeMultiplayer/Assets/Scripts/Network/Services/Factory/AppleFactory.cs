using System.Collections.Generic;
using Gameplay;
using Gameplay.Common;
using Gameplay.Environment;
using Infrastructure;
using Network.Extensions;
using Network.Schemas;
using UnityEngine;

namespace Network.Services.Factory
{
    public class AppleFactory
    {
        private const string ApplePath = "Apple/Apple";

        private readonly Assets _assets;
        private readonly Dictionary<string, Apple> _apples;

        public AppleFactory(Assets assets)
        {
            _assets = assets;
            _apples = new Dictionary<string, Apple>();
        }

        public Apple CreateApple(string key, AppleSchema schema)
        {
            var apple = _assets.Instantiate<Apple>(ApplePath, schema.position.ToVector3(), Quaternion.identity, null);
            apple.GetComponent<UniqueId>().Construct(key);
            schema.OnPositionChange(apple.ChangePosition);
            _apples[key] = apple;
            return apple;
        }

        public void RemoveApple(string key)
        {
            if (!_apples.TryGetValue(key, out var apple)) 
                return;
            
            _apples.Remove(key);
            Object.Destroy(apple);
        }
    }
}