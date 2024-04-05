using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class ConnectionScreen : GameScreen
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
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
                _errorLabel.text = "EMPTY USERNAME!";
                _errorLabel.Show();
                return;
            }

            SetEnabledButtons(false);
            var result = await _gameBootstrapper.Connect();

            if (result.IsSuccess)
            {
                Connected?.Invoke();
                Hide();
            }
            else
            {
                _errorLabel.text = "NO CONNECTION!";
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
