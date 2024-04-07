using Reflex.Attributes;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;
        
        [Inject]
        public void Construct(Game game) => 
            _game = game;

        private void Start() => 
            _game.Start();

        private async void OnDestroy() => 
            await _game.Disconnect();
    }
}