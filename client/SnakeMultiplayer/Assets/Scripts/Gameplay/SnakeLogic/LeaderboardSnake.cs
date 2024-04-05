using System;
using Gameplay.Common;
using Network.Schemas;
using Reflex.Attributes;
using Services;
using Services.Leaders;
using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class LeaderboardSnake : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;
        
        private LeaderboardService _leaderboard;
        private StaticDataService _staticData;
        private Action _dispose;
        
        [Inject]
        public void Construct(LeaderboardService leaderboard, StaticDataService staticData)
        {
            _leaderboard = leaderboard;
            _staticData = staticData;
        }

        public void Initialize(PlayerSchema schema)
        {
            var skin = _staticData.ForSnakeSkin(schema.skinId);
            _leaderboard.CreateLeader(_uniqueId.Value, schema.username, schema.score, skin.color);
            _dispose = schema.OnScoreChange(OnScoreUpdated);
        }

        private void OnDestroy()
        {
            _leaderboard.RemoveLeader(_uniqueId.Value);
            _dispose?.Invoke();
        }

        private void OnScoreUpdated(ushort current, ushort previous) => 
            _leaderboard.UpdateLeader(_uniqueId.Value, current);
    }
}