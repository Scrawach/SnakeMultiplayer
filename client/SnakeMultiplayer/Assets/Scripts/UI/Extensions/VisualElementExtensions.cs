using UnityEngine.UIElements;

namespace UI.Extensions
{
    public static class VisualElementExtensions
    {
        public static void Hide(this VisualElement element) => 
            element.style.display = DisplayStyle.None;

        public static void Show(this VisualElement element) => 
            element.style.display = DisplayStyle.Flex;
    }
}