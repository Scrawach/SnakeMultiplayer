using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.Leaders
{
    public class LeaderboardService
    {
        private readonly Dictionary<string, LeaderInfo> _leaders;

        public LeaderboardService() => 
            _leaders = new Dictionary<string, LeaderInfo>();

        public event Action Updated;
        
        public void CreateLeader(string playerId, string username, int score, Color color)
        {
            _leaders[playerId] = new LeaderInfo()
            {
                Position = _leaders.Count + 1,
                Username = username,
                Score = score,
                Color = color
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

        public IEnumerable<LeaderInfo> GetLeadersSortedByPosition() => 
            SortByPosition(_leaders.Values);

        private IEnumerable<LeaderInfo> SortByPosition(IEnumerable<LeaderInfo> leaders)
        {
            var orderedLeaders = leaders.OrderByDescending(leader => leader.Score);
            var position = 1;
            foreach (var orderedLeader in orderedLeaders)
            {
                orderedLeader.Position = position;
                position++;
                yield return orderedLeader;
            }
        }
    }
}