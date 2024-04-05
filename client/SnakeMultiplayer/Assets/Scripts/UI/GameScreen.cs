using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract class GameScreen : MonoBehaviour
    {
        [SerializeField] private string _menuName;
        [SerializeField] private UIDocument _rootDocument;

        protected VisualElement Screen { get; private set; }
        
        protected virtual void Awake() => 
            Screen = _rootDocument.rootVisualElement.Q(_menuName);

        public virtual void Show() => 
            Screen.Show();

        public virtual void Hide() => 
            Screen.Hide();

        public static void ShowVisualElement(VisualElement visualElement, bool state) => 
            visualElement.style.display = (state) ? DisplayStyle.Flex : DisplayStyle.None;
    }
}