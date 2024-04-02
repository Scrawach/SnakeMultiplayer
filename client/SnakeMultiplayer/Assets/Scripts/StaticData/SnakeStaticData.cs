using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "SnakeStaticData", menuName = "Snake Data", order = 0)]
    public class SnakeStaticData : ScriptableObject
    {
        public SnakeData Data;
        public SnakeSkins Skins;
    }
}