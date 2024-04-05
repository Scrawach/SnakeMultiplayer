using Services.Leaders;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class LeaderboardItem : VisualElement
    {
        private readonly Label _positionField;
        private readonly Label _usernameField;
        private readonly Label _scoreField;
        private readonly VisualElement _numberBackground;
        
        public LeaderboardItem(VisualTreeAsset asset)
        {
            asset.CloneTree(this);
            _positionField = this.Q<Label>("position-label");
            _usernameField = this.Q<Label>("username-label");
            _scoreField = this.Q<Label>("score-label");
            _numberBackground = this.Q<VisualElement>("number-background");
        }

        public void Update(LeaderInfo info)
        {
            _positionField.text = info.Position.ToString();
            _usernameField.text = info.Username;
            _scoreField.text = info.Score.ToString();
            _numberBackground.style.backgroundColor = new StyleColor(info.Color);
        }
    }
}