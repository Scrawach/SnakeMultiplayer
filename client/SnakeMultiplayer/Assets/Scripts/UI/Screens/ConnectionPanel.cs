using System;
using UI.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class ConnectionPanel
    {
        private readonly TextField _usernameField;
        private readonly Button _connectButton;
        private readonly Button _quitButton;
        private readonly Label _errorLabel;

        private Action _onUsernameSubmit;
        
        public ConnectionPanel(VisualElement parent)
        {
            _usernameField = parent.Q<TextField>("username-field");
            _connectButton = parent.Q<Button>("connect-button");
            _quitButton = parent.Q<Button>("quit-button");
            _errorLabel = parent.Q<Label>("error-label");
            
            _usernameField.RegisterCallback(OnUsernameSubmitted());
        }

        public string Username => _usernameField.value;
        
        public event Action QuitClicked
        {
            add => _quitButton.clicked += value;
            remove => _quitButton.clicked -= value;
        }
        
        public event Action ConnectClicked
        {
            add
            {
                _connectButton.clicked += value;
                _onUsernameSubmit += value;
            }
            remove
            {
                _connectButton.clicked -= value;
                _onUsernameSubmit -= value;
            }
        }

        public void HideError() => 
            _errorLabel.Hide();

        public void ShowError(string error)
        {
            _errorLabel.text = error;
            _errorLabel.Show();
        }
        
        public void BlockButtons() => 
            SetEnabledButtons(false);

        public void UnblockButtons() => 
            SetEnabledButtons(true);

        private EventCallback<KeyDownEvent> OnUsernameSubmitted() =>
            evt =>
            {
                if (evt.character != '\n') 
                    return;

                _onUsernameSubmit?.Invoke();
            };
        
        private void SetEnabledButtons(bool isEnable)
        {
            Debug.Log($"set enable {isEnable}");
            _usernameField.SetEnabled(isEnable);
            _connectButton.SetEnabled(isEnable);
            _quitButton.SetEnabled(isEnable);
        }
    }
}