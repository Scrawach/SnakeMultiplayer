using Gameplay.Animations;
using Infrastructure;
using UnityEngine;

namespace Network.Services.Factory
{
    public class VfxFactory
    {
        private const string DeathVfx = "Snake/SnakeDeathEffect";
        
        private readonly Assets _assets;

        public VfxFactory(Assets assets) => 
            _assets = assets;

        public SnakeDeathEffect CreateDeathVfx(Vector3 position, Material skin)
        {
            var effect = _assets.Instantiate<SnakeDeathEffect>(DeathVfx, position, Quaternion.identity, null);
            effect.Construct(skin);
            return effect;
        }
    }
}