using System.Collections.Generic;
using Colyseus;
using Network.Extensions;
using UnityEngine;

namespace Network.Services
{
    public class NetworkTransmitter : INetworkRoomHandler
    {
        private const string MovementEndPoint = "move";
        
        private ColyseusRoom<GameRoomState> _room;

        public void Handle(ColyseusRoom<GameRoomState> room)
        {
            _room = room;
        }

        public void Dispose()
        {
            _room = null;
        }

        public void SendPosition(Vector3 position)
        {
            var message = new Dictionary<string, object>()
            {
                [nameof(position)] = position.ToVector2Data()
            };
            _room.Send(MovementEndPoint, message);
        }
    }
}