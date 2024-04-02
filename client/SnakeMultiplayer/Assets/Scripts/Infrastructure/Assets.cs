using UnityEngine;

namespace Infrastructure
{
    public class Assets
    {
        private readonly Injector _injector;

        public Assets(Injector injector) => 
            _injector = injector;

        public TAsset Load<TAsset>(string path) where TAsset : Object => 
            Resources.Load<TAsset>(path);

        public TAsset Instantiate<TAsset>(string path, Vector3 position, Quaternion rotation, Transform parent) 
            where TAsset : Object =>
            _injector.Inject(Object.Instantiate(Load<TAsset>(path), position, rotation, parent));
    }
}