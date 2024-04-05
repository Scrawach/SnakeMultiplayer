using System;
using UI.Screens;
using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private ConnectionScreen _connectionScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;

        private void Start()
        {
            _connectionScreen.Show();
            _leaderboardScreen.Hide();
        }

        private void OnEnable() => 
            _connectionScreen.Connected += OnStartConnection;

        private void OnDisable() => 
            _connectionScreen.Connected -= OnStartConnection;

        private void OnStartConnection()
        {
            _leaderboardScreen.Show();
        }
    }
}