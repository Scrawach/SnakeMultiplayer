using UnityEngine;

namespace Network.Extensions
{
    public static class Vector2DataExtensions
    {
        public static Vector3 ToVector3(this Vector2Data data) => 
            new Vector3(data.x, 0, data.y);
    }
}