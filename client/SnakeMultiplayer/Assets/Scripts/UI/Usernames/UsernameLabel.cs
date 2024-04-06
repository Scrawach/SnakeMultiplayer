using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Usernames
{
    public class UsernameLabel : VisualElement
    {
        private readonly VisualElement _container;
        private readonly Label _usernameLabel;

        public UsernameLabel(VisualTreeAsset asset)
        {
            asset.CloneTree(this);
            _container = this.Q<VisualElement>("container");
            _usernameLabel = this.Q<Label>("username-label");
        }

        public void SetUsername(string username) => 
            _usernameLabel.text = username;

        public void SetLabelColor(Color color) => 
            _usernameLabel.style.color = color;

        public void SetScreenPosition(Vector2 position)
        {
            style.left = position.x - _container.worldBound.width / 2;
            style.top = Screen.height - position.y - _container.worldBound.height / 2;
        }
    }
}