using UnityEngine;

namespace Gameplay
{
    public class SnakeHeadTrigger : MonoBehaviour
    {
        [SerializeField] private SnakeDeath _snakeDeath;
        [SerializeField] private SphereCollider _mouthCollider;
        [SerializeField] private LayerMask _targetMask;

        private readonly Collider[] _colliders = new Collider[3];

        private void FixedUpdate()
        {
            var hits = OverlapHits();
            for (var i = 0; i < hits; i++) 
                ProcessCollision(_colliders[i]);
        }

        private int OverlapHits() =>
            Physics.OverlapSphereNonAlloc(_mouthCollider.transform.position, _mouthCollider.radius, _colliders, _targetMask);

        private void ProcessCollision(Component target)
        {
            if (target.TryGetComponent(out Apple apple))
            {
                apple.Collect();
            }
            else
            {
                _snakeDeath.Die();
            }
        }
    }
}