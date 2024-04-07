using Network.Services;
using Network.Services.Factory;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Reflex.Core;
using Services;
using Services.Leaders;
using UI.Factory;
using UnityEngine;

namespace Infrastructure
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(Injector));
            builder.AddSingleton(typeof(CameraProvider));
            builder.AddSingleton(typeof(Game));
            builder.AddSingleton(typeof(InputService));
            builder.AddSingleton(typeof(Assets));
            builder.AddSingleton(typeof(StaticDataService));

            builder.AddSingleton(typeof(UIFactory));
            builder.AddSingleton(typeof(LeaderboardService));
            builder.AddSingleton(typeof(SnakesFactory));
            builder.AddSingleton(typeof(AppleFactory));
            builder.AddSingleton(typeof(VfxFactory));
            builder.AddSingleton(typeof(SnakesDestruction));
            
            builder.AddSingleton(typeof(SnakesRegistry));
            builder.AddSingleton(typeof(NetworkClient));
            builder.AddSingleton(typeof(NetworkPlayersListener));
            builder.AddSingleton(typeof(NetworkApplesListener));
            builder.AddSingleton(typeof(NetworkStateInitializer), typeof(INetworkRoomHandler));
            builder.AddSingleton(typeof(NetworkStatusProvider), typeof(INetworkRoomHandler), typeof(INetworkStatusProvider));
            builder.AddSingleton(typeof(NetworkTransmitter), typeof(NetworkTransmitter), typeof(INetworkRoomHandler));
            builder.AddSingleton(typeof(NetworkGameFactory));
        }
    }
}