using System;
using System.Collections.Generic;
using Extensions;
using Network.Schemas;
using Network.Services.Factory;
using Services.Leaders;

namespace Network.Services.RoomHandlers
{
    public class NetworkPlayersListener : IDisposable
    {
        private readonly NetworkGameFactory _networkGameFactory;
        private readonly List<Action> _disposes;

        public NetworkPlayersListener(NetworkGameFactory networkGameFactory)
        {
            _networkGameFactory = networkGameFactory;
            _disposes = new List<Action>();
        }

        public void Initialize(GameRoomState state)
        {
            state.players.OnAdd(OnPlayerAdded).AddTo(_disposes);
            state.players.OnRemove(OnPlayerRemoved).AddTo(_disposes);
        }
        
        public void Dispose()
        {
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }
        
        private void OnPlayerAdded(string key, PlayerSchema player) => 
            _networkGameFactory.CreateSnake(key, player);

        private void OnPlayerRemoved(string key, PlayerSchema player) => 
            _networkGameFactory.RemoveSnake(key);
    }
}