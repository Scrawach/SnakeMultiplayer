using Infrastructure;
using UnityEngine;

namespace UI.Factory
{
    public class UIFactory
    {
        private const string UIRootPath = "UI/UI Root";

        private readonly Assets _assets;

        public UIFactory(Assets assets) => 
            _assets = assets;

        public UIRoot CreateUIRoot() => 
            _assets.Instantiate<UIRoot>(UIRootPath, Vector3.zero, Quaternion.identity, null);
    }
}