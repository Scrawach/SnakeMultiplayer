using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;

namespace UI.Screens
{
    public class ConnectionScreen : GameScreen
    {
        [SerializeField] private string _emptyUsernameMessage = "EMPTY USERNAME!";

        private Game _game;
        private ConnectionPanel _connectionPanel;

        [Inject]
        public void Construct(Game game) => 
            _game = game;

        public event Action Connected;
        
        protected override void Awake()
        {
            base.Awake();
            _connectionPanel = new ConnectionPanel(Screen);
        }
        
        private void OnEnable()
        {
            _connectionPanel.ConnectClicked += OnConnectClicked;
            _connectionPanel.QuitClicked += OnQuitClicked;
        }
        
        private void OnDisable()
        {
            _connectionPanel.ConnectClicked -= OnConnectClicked;
            _connectionPanel.QuitClicked -= OnQuitClicked;
        }

        private void OnConnectClicked() => 
            ConnectServer().Forget();
                
        private void OnQuitClicked() => 
            Application.Quit();

        private async UniTask ConnectServer()
        {
            _connectionPanel.HideError();
            
            if (string.IsNullOrWhiteSpace(_connectionPanel.Username))
            {
                _connectionPanel.ShowError(_emptyUsernameMessage);
                return;
            }

            _connectionPanel.BlockButtons();
            var result = await _game.Connect(_connectionPanel.Username);

            if (result.IsSuccess)
                Connected?.Invoke();
            else
                _connectionPanel.ShowError(result.Message);

            _connectionPanel.UnblockButtons();
        }
    }
}
