using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class Tail : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private List<Transform> _details;
        [SerializeField] private float _detailDistance = 1;

        private List<Vector3> _history = new List<Vector3>();

        private void Awake()
        {
            _history.Add(_head.position);
            foreach (var detail in _details) 
                _history.Add(detail.position);
        }

        private void Update()
        {
            var distance = Vector3.Distance(_head.position, _history[0]);
            while (distance > _detailDistance)
            {
                var direction = (_head.position - _history[0]).normalized;
                _history.Insert(0, _history[0] + direction * _detailDistance);
                _history.RemoveAt(_history.Count - 1);
                distance -= _detailDistance;
            }

            for (var i = 0; i < _details.Count; i++)
            {
                _details[i].position = Vector3.Lerp(_history[i + 1], _history[i], distance / _detailDistance);
                var direction = (_history[i] - _history[i + 1]).normalized;
                _details[i].position += direction * Time.deltaTime;
            }
        }
    }
}