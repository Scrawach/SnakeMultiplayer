using Network.Schemas;
using UnityEngine;

namespace Network.Extensions
{
    public static class Vector2DataExtensions
    {
        public static Vector3 ToVector3(this Vector2Data data) => 
            new Vector3(data.x, 0, data.y);

        public static Vector2Data ToVector2Data(this Vector3 data) =>
            new Vector2Data() { x = data.x, y = data.z };
    }
}