using System;
using System.Collections.Generic;
using Extensions;
using Gameplay.Common;
using Network.Extensions;
using Network.Schemas;
using Network.Services.Factory;
using Reflex.Attributes;
using Services.Leaders;
using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class RemoteSnake : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private UniqueId _uniqueId;

        private readonly List<Action> _disposes = new List<Action>();
        
        private SnakesFactory _snakesFactory;
        private LeaderboardService _leaderboard;

        [Inject]
        public void Construct(SnakesFactory snakesFactory, LeaderboardService leaderboard)
        {
            _snakesFactory = snakesFactory;
            _leaderboard = leaderboard;
        }

        public void Initialize(PlayerSchema schema)
        {
            _leaderboard.CreateLeader(_uniqueId.Value, schema);
            schema.OnPositionChange(ChangePosition).AddTo(_disposes);
            schema.OnSizeChange(ChangeSize).AddTo(_disposes);
            schema.OnScoreChange(ChangeScore).AddTo(_disposes);
        }

        private void OnDestroy()
        {
            _leaderboard.RemoveLeader(_uniqueId.Value);
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }

        private void ChangeScore(ushort current, ushort previous) => 
            _leaderboard.UpdateLeader(_uniqueId.Value, current);

        private void ChangePosition(Vector2Schema current, Vector2Schema previous) => 
            _snake.LookAt(current.ToVector3());

        private void ChangeSize(byte current, byte previous)
        {
            if (_snake.Body.Size == current)
                return;

            ProcessChangeSizeTo(current);
        }

        private void ProcessChangeSizeTo(int target)
        {
            var difference = _snake.Body.Size - target;

            if (difference < 0)
                _snakesFactory.AddSnakeDetail(_uniqueId.Value, -difference);
            else
                _snakesFactory.RemoveSnakeDetails(_uniqueId.Value, difference);
        }
    }
}