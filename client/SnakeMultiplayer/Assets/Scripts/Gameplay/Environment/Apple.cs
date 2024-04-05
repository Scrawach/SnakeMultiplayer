using Gameplay.Common;
using Network.Extensions;
using Network.Schemas;
using Network.Services.Factory;
using Network.Services.RoomHandlers;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay.Environment
{
    public class Apple : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;
        
        private NetworkTransmitter _transmitter;
        private VfxFactory _vfxFactory;

        [Inject]
        public void Construct(NetworkTransmitter transmitter, VfxFactory vfxFactory)
        {
            _transmitter = transmitter;
            _vfxFactory = vfxFactory;
        }

        public void Collect()
        {
            _transmitter.SendAppleCollect(_uniqueId.Value);
            _vfxFactory.CreateAppleDeathVfx(transform.position);
            gameObject.SetActive(false);
        }

        public void ChangePosition(Vector2Schema current, Vector2Schema previous)
        {
            transform.position = current.ToVector3();
            gameObject.SetActive(true);
        }
    }
}