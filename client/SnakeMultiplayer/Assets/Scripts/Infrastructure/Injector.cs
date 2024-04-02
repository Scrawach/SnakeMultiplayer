using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace Infrastructure
{
    public class Injector
    {
        private readonly Container _container;

        public Injector(Container container) => 
            _container = container;
        public TObject Inject<TObject>(TObject obj) where TObject : Object
        {
            switch (obj)
            {
                case GameObject gameObject:
                    GameObjectInjector.InjectRecursive(gameObject, _container);
                    break;
                case Component component:
                    GameObjectInjector.InjectRecursive(component.gameObject, _container);
                    break;
                default:
                    AttributeInjector.Inject(obj, _container);
                    break;
            }

            return obj;
        }
    }
}