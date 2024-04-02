using System;
using Gameplay.SnakeLogic;

namespace Network.Services.Snakes
{
    public class RemoteSnakeInfo
    {
        public Snake Snake;
        public Action[] Disposes;
    }
}