using System;
using Colyseus;

namespace Network.Services
{
    public interface INetworkRoomHandler : IDisposable
    {
        void Handle(ColyseusRoom<GameRoomState> room);
    }
}