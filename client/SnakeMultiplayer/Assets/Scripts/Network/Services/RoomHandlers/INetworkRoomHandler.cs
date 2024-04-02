using System;
using Colyseus;
using Network.Schemas;

namespace Network.Services.RoomHandlers
{
    public interface INetworkRoomHandler : IDisposable
    {
        void Handle(ColyseusRoom<GameRoomState> room);
    }
}