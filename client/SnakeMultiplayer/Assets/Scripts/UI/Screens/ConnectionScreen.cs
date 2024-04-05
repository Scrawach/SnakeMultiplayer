using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UI.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class ConnectionScreen : GameScreen
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        [SerializeField] private string _hasNotConnectionMessage = "NO CONNECTION!";
        [SerializeField] private string _emptyUsernameMessage = "EMPTY USERNAME!";
        
        private TextField _usernameField;
        private Button _connectButton;
        private Button _quitButton;
        private Label _errorLabel;

        public event Action Connected;
        
        protected override void Awake()
        {
            base.Awake();
            _usernameField = Screen.Q<TextField>("username-field");
            _connectButton = Screen.Q<Button>("connect-button");
            _quitButton = Screen.Q<Button>("quit-button");
            _errorLabel = Screen.Q<Label>("error-label");
        }
        
        private void OnEnable()
        {
            _usernameField.RegisterCallback(OnUsernameSubmitted());
            _connectButton.clicked += OnConnectClicked;
            _quitButton.clicked += OnQuitClicked;
        }
        
        private void OnDisable()
        {
            _usernameField.UnregisterCallback(OnUsernameSubmitted());
            _connectButton.clicked -= OnConnectClicked;
            _quitButton.clicked -= OnQuitClicked;
        }

        private EventCallback<KeyDownEvent> OnUsernameSubmitted() =>
            evt =>
            {
                if (evt.character != '\n') 
                    return;

                ConnectServer().Forget();
            };

        private void OnConnectClicked() => 
            ConnectServer().Forget();
                
        private void OnQuitClicked() => 
            Application.Quit();

        private async UniTask ConnectServer()
        {
            _errorLabel.Hide();
            
            if (string.IsNullOrWhiteSpace(_usernameField.value))
            {
                _errorLabel.text = _emptyUsernameMessage;
                _errorLabel.Show();
                return;
            }

            SetEnabledButtons(false);
            var result = await _gameBootstrapper.Connect(_usernameField.value);

            if (result.IsSuccess)
            {
                Connected?.Invoke();
                Hide();
            }
            else
            {
                _errorLabel.text = _hasNotConnectionMessage;
                _errorLabel.Show();
            }
            
            SetEnabledButtons(true);
        }
        
        private void SetEnabledButtons(bool isEnable)
        {
            _usernameField.SetEnabled(isEnable);
            _connectButton.SetEnabled(isEnable);
            _quitButton.SetEnabled(isEnable);
        }
    }
}