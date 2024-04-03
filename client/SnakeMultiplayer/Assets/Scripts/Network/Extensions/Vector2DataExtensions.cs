using Network.Schemas;
using UnityEngine;

namespace Network.Extensions
{
    public static class Vector2DataExtensions
    {
        public static Vector3 ToVector3(this Vector2Schema data) => 
            new Vector3(data.x, 0, data.y);

        public static Vector2Schema ToVector2Schema(this Vector3 data) =>
            new Vector2Schema() { x = data.x, y = data.z };
    }
}