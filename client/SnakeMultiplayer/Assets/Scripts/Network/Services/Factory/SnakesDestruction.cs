using System.Linq;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Services;

namespace Network.Services.Factory
{
    public class SnakesDestruction
    {
        private readonly SnakesRegistry _snakes;
        private readonly StaticDataService _staticData;
        private readonly NetworkTransmitter _transmitter;
        private readonly VfxFactory _vfxFactory;

        public SnakesDestruction(SnakesRegistry snakes, StaticDataService staticData, NetworkTransmitter transmitter, VfxFactory vfxFactory)
        {
            _snakes = snakes;
            _staticData = staticData;
            _transmitter = transmitter;
            _vfxFactory = vfxFactory;
        }

        public void Destruct(string snakeId)
        {
            var info = _snakes[snakeId];
            var positions = info.Snake.GetBodyDetailPositions().ToArray();
            var skin = _staticData.ForSnakeSkin(info.Player.skinId);
            
            foreach (var position in positions) 
                _vfxFactory.CreateSnakeDeathVfx(position, skin);

            _transmitter.SendDeathSnakeDetailPositions(snakeId, positions);
        }
    }
}