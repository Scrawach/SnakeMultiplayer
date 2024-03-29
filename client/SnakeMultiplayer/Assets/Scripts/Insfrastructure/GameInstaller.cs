using Reflex.Core;
using Services;
using UnityEngine;

namespace Insfrastructure
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(CameraProvider));
            builder.AddSingleton(typeof(InputService));
        }
    }
}