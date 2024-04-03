using System;
using Gameplay;
using Gameplay.SnakeLogic;
using Network.Schemas;

namespace Network.Services.Snakes
{
    public class RemoteSnakeInfo
    {
        public Snake Snake;
        public PlayerSchema Player;
        public Action[] Disposes;
    }
}