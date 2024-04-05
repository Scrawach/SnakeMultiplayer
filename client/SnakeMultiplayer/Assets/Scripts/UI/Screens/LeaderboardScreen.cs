using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class LeaderboardScreen : GameScreen
    {
        [SerializeField] private VisualTreeAsset _leaderItemTreeAsset;

        private VisualElement _leaderboardContainer;

        protected override void Awake()
        {
            base.Awake();
            _leaderboardContainer = Screen.Q<VisualElement>("leaderboard-content");
        }

        private void Start()
        {
            for (var i = 0; i < 6; i++)
                _leaderboardContainer.Add(CreateLeaderboardItem());
        }

        private VisualElement CreateLeaderboardItem()
        {
            var item = _leaderItemTreeAsset.Instantiate();
            return item;
        }
    }
}