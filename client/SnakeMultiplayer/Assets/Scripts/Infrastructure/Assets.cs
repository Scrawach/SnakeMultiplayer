using UnityEngine;

namespace Infrastructure
{
    public class Assets
    {
        private readonly Injector _injector;

        public Assets(Injector injector) => 
            _injector = injector;

        public TAsset Instantiate<TAsset>(string path, Vector3 position, Quaternion rotation, Transform parent) 
            where TAsset : Object
        {
            var resource = Resources.Load<TAsset>(path);
            return _injector.Inject(Object.Instantiate(resource, position, rotation, parent));
        }
    }
}