using System;
using System.Collections.Generic;
using Colyseus;
using Cysharp.Threading.Tasks;
using Services;

namespace Network.Services
{
    public class NetworkClient
    {
        private const string GameRoomName = "GameRoom";
        private readonly StaticDataService _staticData;
        private readonly IEnumerable<INetworkRoomHandler> _handlers;

        private ColyseusRoom<GameRoomState> _room;
        
        public NetworkClient(StaticDataService staticData, IEnumerable<INetworkRoomHandler> handlers)
        {
            _staticData = staticData;
            _handlers = handlers;
        }

        public async UniTask<ConnectionResult> Connect()
        {
            var result = await TryConnect();

            if (result.IsFailure)
                return result;
            
            foreach (var handler in _handlers) 
                handler.Handle(_room);

            return result;
        }

        public async UniTask Disconnect()
        {
            if (_room == null)
                return;

            await _room.Leave();
            
            foreach (var handler in _handlers) 
                handler.Dispose();
        }

        private async UniTask<ConnectionResult> TryConnect()
        {
            var settings = _staticData.ForConnection();
            var client = new ColyseusClient(settings);

            try
            {
                _room = await client.JoinOrCreate<GameRoomState>(GameRoomName);
            }
            catch (Exception exception)
            {
                return ConnectionResult.Failure(exception.Message);
            }
            
            return ConnectionResult.Success();
        }
    }
}