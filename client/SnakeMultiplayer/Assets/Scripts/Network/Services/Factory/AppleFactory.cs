using Gameplay;
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

        public AppleFactory(Assets assets) => 
            _assets = assets;

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