using Colyseus;
using Infrastructure;
using StaticData;
using UnityEngine;

namespace Services
{
    public class StaticDataService
    {
        private const string ConnectionSettingPath = "StaticData/ColyseusSettings";
        private const string SnakeDataPath = "StaticData/SnakeStaticData";
        
        private readonly Assets _assets;
        private ColyseusSettings _connectionSettings;
        private SnakeStaticData _snakeStaticData;

        public StaticDataService(Assets assets) => 
            _assets = assets;

        public void Load()
        {
            _connectionSettings = _assets.Load<ColyseusSettings>(ConnectionSettingPath);
            _snakeStaticData = _assets.Load<SnakeStaticData>(SnakeDataPath);
        }

        public ColyseusSettings ForConnection() => 
            _connectionSettings;

        public SnakeData ForSnake() =>
            _snakeStaticData.Data;

        public Material ForSnakeSkin(int index)
        {
            var materials = _snakeStaticData.Skins;
            var clampedIndex = index % materials.Materials.Length;
            return materials.Materials[clampedIndex];
        }

    }
}