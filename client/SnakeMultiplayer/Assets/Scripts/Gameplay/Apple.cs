using System;
using Network.Extensions;
using Network.Schemas;
using Network.Services.RoomHandlers;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay
{
    public class Apple : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;
        private NetworkTransmitter _transmitter;

        [Inject]
        public void Construct(NetworkTransmitter transmitter) => 
            _transmitter = transmitter;

        public void Collect()
        {
            _transmitter.SendAppleCollect(_uniqueId.Value);
            gameObject.SetActive(false);
        }

        public void ChangePosition(Vector2Schema current, Vector2Schema previous)
        {
            transform.position = current.ToVector3();
            gameObject.SetActive(true);
        }
    }
}