using Colyseus;
using Infrastructure;

namespace Services
{
    public class StaticDataService
    {
        private const string ConnectionSettingPath = "ColyseusSettings";
        
        private readonly Assets _assets;
        private ColyseusSettings _connectionSettings;

        public StaticDataService(Assets assets) => 
            _assets = assets;

        public void Load() => 
            _connectionSettings = _assets.Load<ColyseusSettings>(ConnectionSettingPath);

        public ColyseusSettings ForConnection() => 
            _connectionSettings;
    }
}