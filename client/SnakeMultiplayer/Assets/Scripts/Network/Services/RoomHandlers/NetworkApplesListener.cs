using System;
using System.Collections.Generic;
using Extensions;
using Network.Schemas;
using Network.Services.Factory;

namespace Network.Services.RoomHandlers
{
    public class NetworkApplesListener : IDisposable
    {
        private readonly NetworkGameFactory _networkGameFactory;
        private readonly List<Action> _disposes;

        public NetworkApplesListener(NetworkGameFactory networkGameFactory)
        {
            _networkGameFactory = networkGameFactory;
            _disposes = new List<Action>();
        }

        public void Initialize(GameRoomState state)
        {
            state.apples.OnAdd(OnAppleAdded).AddTo(_disposes);
            state.apples.OnRemove(OnAppleRemoved).AddTo(_disposes);
        }

        public void Dispose()
        {
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }

        private void OnAppleAdded(string key, AppleSchema schema) => 
            _networkGameFactory.CreateApple(key, schema);

        private void OnAppleRemoved(string key, AppleSchema schema) => 
            _networkGameFactory.RemoveApple(key);
    }
}