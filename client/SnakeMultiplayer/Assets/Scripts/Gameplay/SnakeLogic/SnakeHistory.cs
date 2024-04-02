using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class SnakeHistory
    {
        private readonly List<Vector3> _positions;
        private readonly List<Quaternion> _rotations;

        public SnakeHistory() : this(16) { }
        
        public SnakeHistory(int size)
        {
            _positions = new List<Vector3>(size);
            _rotations = new List<Quaternion>(size);
        }

        public Vector3 FirstPosition => _positions[0];
        
        public void Add(Transform transform) => 
            Add(transform.position, transform.rotation);

        public void Add(Vector3 position, Quaternion rotation)
        {
            _positions.Add(position);
            _rotations.Add(rotation);
        }

        public void AddToFront(Vector3 position, Quaternion rotation)
        {
            _positions.Insert(0, position);
            _rotations.Insert(0, rotation);
            RemoveLast();
        }

        public (Vector3 position, Quaternion rotation) Lerp(int index, float percent) => 
        (
            Vector3.Lerp(_positions[index + 1], _positions[index], percent),
            Quaternion.Lerp(_rotations[index + 1], _rotations[index], percent)
        );

        public void RemoveLast()
        {
            _positions.RemoveAt(_positions.Count - 1);
            _rotations.RemoveAt(_rotations.Count - 1);
        }
    }
}