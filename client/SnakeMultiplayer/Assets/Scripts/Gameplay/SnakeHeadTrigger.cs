using Gameplay.SnakeLogic;
using UnityEngine;

namespace Gameplay
{
    public class SnakeHeadTrigger : MonoBehaviour
    {
        [SerializeField] private SnakeHead _head;
        [SerializeField] private float _overlapRadius = 0.5f;
        [SerializeField] private LayerMask _targetMask;

        private readonly Collider[] _colliders = new Collider[3];
        
        private void FixedUpdate()
        {
            var hits = OverlapHits();
            for (var i = 0; i < hits; i++) 
                ProcessCollision(_colliders[i]);
        }

        private int OverlapHits() => 
            Physics.OverlapSphereNonAlloc(_head.transform.position, _overlapRadius, _colliders, _targetMask);

        private void ProcessCollision(Component target)
        {
            if (target.TryGetComponent(out Apple apple))
            {
                apple.Collect();
            }
        }
    }
}