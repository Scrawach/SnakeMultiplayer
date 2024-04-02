using Colyseus;
using Network.Schemas;

namespace Network.Services.RoomHandlers
{
    public class NetworkStatusProvider : INetworkRoomHandler, INetworkStatusProvider
    {
        private ColyseusRoom<GameRoomState> _room;

        public void Handle(ColyseusRoom<GameRoomState> room)
        {
            _room = room;
            SessionId = _room.SessionId;
        }
        public string SessionId { get; private set; }
        
        public bool IsPlayer(string sessionId) => 
            sessionId == SessionId;
        
        public void Dispose()
        {
            _room = null;
            SessionId = string.Empty;
        }

    }
}