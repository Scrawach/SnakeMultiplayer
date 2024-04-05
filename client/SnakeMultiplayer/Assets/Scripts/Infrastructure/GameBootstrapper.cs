using Cysharp.Threading.Tasks;
using Network.Services;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private NetworkClient _client;
        private StaticDataService _staticData;
        
        [Inject]
        public void Construct(NetworkClient client, StaticDataService staticData)
        {
            _client = client;
            _staticData = staticData;
        }

        private async void Start() => 
            _staticData.Load();

        public async UniTask<ConnectionResult> Connect() => 
            await _client.Connect();
        
        private async void OnDestroy() => 
            await _client.Disconnect();
    }
}