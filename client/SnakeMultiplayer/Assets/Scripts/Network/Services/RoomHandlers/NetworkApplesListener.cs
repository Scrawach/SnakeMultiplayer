using System;
using System.Collections.Generic;
using Extensions;
using Network.Schemas;
using Network.Services.Factory;

namespace Network.Services.RoomHandlers
{
    public class NetworkApplesListener : IDisposable
    {
        private readonly AppleFactory _appleFactory;
        private readonly List<Action> _disposes;

        public NetworkApplesListener(AppleFactory appleFactory)
        {
            _appleFactory = appleFactory;
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
            _appleFactory.CreateApple(key, schema);

        private void OnAppleRemoved(string key, AppleSchema schema) => 
            _appleFactory.RemoveApple(key);
    }
}