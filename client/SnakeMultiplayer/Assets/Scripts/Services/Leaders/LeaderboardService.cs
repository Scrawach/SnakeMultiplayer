using System;
using System.Collections.Generic;
using System.Linq;
using Network.Schemas;
using UnityEngine;

namespace Services.Leaders
{
    public class LeaderboardService
    {
        private readonly StaticDataService _staticData;
        private readonly Dictionary<string, LeaderInfo> _leaders;

        public LeaderboardService(StaticDataService staticData)
        {
            _staticData = staticData;
            _leaders = new Dictionary<string, LeaderInfo>();
        }

        public event Action Updated;
        
        public void CreateLeader(string playerId, PlayerSchema schema)
        {
            var skin = _staticData.ForSnakeSkin(schema.skinId);
            
            _leaders[playerId] = new LeaderInfo()
            {
                Position = _leaders.Count + 1,
                Username = schema.username,
                Score = schema.score,
                Color = skin.color,
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