using Services.Leaders;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class LeaderboardItem : VisualElement
    {
        private readonly Label _positionField;
        private readonly Label _usernameField;
        private readonly Label _scoreField;
        
        public LeaderboardItem(VisualTreeAsset asset)
        {
            asset.CloneTree(this);
            _positionField = this.Q<Label>("position-label");
            _usernameField = this.Q<Label>("username-label");
            _scoreField = this.Q<Label>("score-label");
        }

        public void Update(LeaderInfo info)
        {
            _positionField.text = info.Position.ToString();
            _usernameField.text = info.Username;
            _scoreField.text = info.Score.ToString();
        }
    }
}