using UnityEngine;

namespace Gameplay.Animations
{
    public class SnakeDeathEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystemRenderer _vfxRenderer;
        
        public void Construct(Material skin) => 
            _vfxRenderer.material = skin;
    }
}