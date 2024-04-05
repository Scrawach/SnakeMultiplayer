using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Leaders
{
    public class LeaderboardService
    {
        private readonly Dictionary<string, LeaderInfo> _leaders;

        public LeaderboardService() => 
            _leaders = new Dictionary<string, LeaderInfo>();

        public event Action Updated;
        
        public void CreateLeader(string playerId, string username, int score)
        {
            _leaders[playerId] = new LeaderInfo()
            {
                Position = _leaders.Count + 1,
                Username = username,
                Score = score
            };
            Updated?.Invoke();
        }

        public void RemoveLeader(string playerId)
        {
            _leaders.Remove(playerId);
            Updated?.Invoke();
        }

        public void UpdateLeader(string playerId, int score)
        {
            _leaders[playerId].Score = score;
            Updated?.Invoke();
        }

        public IEnumerable<LeaderInfo> GetLeadersSortedByPosition()
        {
            return _leaders.Values;
        }
    }
}