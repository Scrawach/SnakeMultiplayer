using System.Collections.Generic;
using System.Linq;
using Colyseus;
using Network.Extensions;
using Network.Schemas;
using UnityEngine;

namespace Network.Services.RoomHandlers
{
    public class NetworkTransmitter : INetworkRoomHandler
    {
        private const string MovementEndPoint = "move";
        private const string AppleCollectEndPoint = "collectApple";
        private const string DeathSnakePositionsEndPoint = "snakeDeath";
        
        private ColyseusRoom<GameRoomState> _room;

        public void Handle(ColyseusRoom<GameRoomState> room)
        {
            _room = room;
        }

        public void Dispose()
        {
            _room = null;
        }

        public void SendAppleCollect(string appleId)
        {
            var message = new Dictionary<string, string>()
            {
                [nameof(appleId)] = appleId
            };
            _room.Send(AppleCollectEndPoint, message);
        }

        public void SendPosition(Vector3 position)
        {
            var message = new Dictionary<string, object>()
            {
                [nameof(position)] = position.ToVector2Schema()
            };
            _room.Send(MovementEndPoint, message);
        }

        public void SendDeathSnakeDetailPositions(string snakeId, IEnumerable<Vector3> positions)
        {
            var message = new Dictionary<string, object>()
            {
                [nameof(snakeId)] = snakeId,
                [nameof(positions)] = positions.Select(position => new Vector2(position.x, position.z))
            };
            _room.Send(DeathSnakePositionsEndPoint, message);
        }
    }
}