using Gameplay.Animations;
using Infrastructure;
using UnityEngine;

namespace Network.Services.Factory
{
    public class VfxFactory
    {
        private const string SnakeDeathVfx = "Snake/SnakeDeathEffect";
        private const string AppleDeathVfx = "Apple/AppleDeathEffect";
        
        private readonly Assets _assets;

        public VfxFactory(Assets assets) => 
            _assets = assets;

        public SnakeDeathEffect CreateSnakeDeathVfx(Vector3 position, Material skin)
        {
            var effect = _assets.Instantiate<SnakeDeathEffect>(SnakeDeathVfx, position, Quaternion.identity, null);
            effect.Construct(skin);
            return effect;
        }

        public GameObject CreateAppleDeathVfx(Vector3 position) => 
            _assets.Instantiate<GameObject>(AppleDeathVfx, position, Quaternion.identity, null);
    }
}