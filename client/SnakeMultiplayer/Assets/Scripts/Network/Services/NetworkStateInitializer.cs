using Colyseus;

namespace Network.Services
{
    public class NetworkStateInitializer : INetworkRoomHandler
    {
        private readonly NetworkPlayersListener _playersListener;
        private ColyseusRoom<GameRoomState> _room;

        public NetworkStateInitializer(NetworkPlayersListener playersListener)
        {
            _playersListener = playersListener;
        }

        public void Handle(ColyseusRoom<GameRoomState> room)
        {
            _room = room;
            _room.OnStateChange += OnStateChanged;
        }
        
        public void Dispose()
        {
            _room.OnStateChange -= OnStateChanged;
            _playersListener.Dispose();
        }

        private void OnStateChanged(GameRoomState state, bool isFirstState)
        {
            if (isFirstState == false)
                return;
            
            _room.OnStateChange -= OnStateChanged;
            _playersListener.Initialize(state);
        }
    }
}