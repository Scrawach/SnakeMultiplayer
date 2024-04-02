using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class SnakeData
    {
        [Min(0)] public float MovementSpeed;
        [Min(0)] public float RotationSpeed;
    }
}