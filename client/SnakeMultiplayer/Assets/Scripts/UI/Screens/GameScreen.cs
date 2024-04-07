using UI.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
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
    }
}