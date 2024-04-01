using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Tail : MonoBehaviour
    {
        [SerializeField] private float _detailDistance = 1;
        
        private readonly Stack<Vector3> _history;

        private void Start() => 
            _history.Push(transform.position);

        private void Update()
        {
            var previousPosition = _history.Peek();
            var distance = Vector3.Distance(transform.position, previousPosition);

            while (distance > _detailDistance)
            {
                var previous = _history.Pop();
                var direction = (previous - transform.position).normalized;
                _history.Push(previous + direction * _detailDistance);
                distance -= _detailDistance;
            }
        }
    }
}