using System;
using Network.Schemas;
using Reflex.Attributes;
using Services.Leaders;
using UnityEngine;

namespace Gameplay
{
    public class LeaderboardSnake : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;
        
        private LeaderboardService _leaderboard;
        private Action _dispose;
        
        [Inject]
        public void Construct(LeaderboardService leaderboard) => 
            _leaderboard = leaderboard;

        public void Initialize(PlayerSchema schema)
        {
            _leaderboard.CreateLeader(_uniqueId.Value, schema.username, schema.score);
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