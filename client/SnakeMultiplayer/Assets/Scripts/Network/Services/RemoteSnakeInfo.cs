using System;
using Gameplay.SnakeLogic;

namespace Network.Services
{
    public class RemoteSnakeInfo
    {
        public Snake Snake;
        public Action[] Disposes;
    }
}