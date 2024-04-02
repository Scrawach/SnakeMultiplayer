using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class SnakeSkin : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _elements;
        
        public void ChangeTo(Material targetMaterial)
        {
            foreach (var element in _elements) 
                element.material = targetMaterial;
        }
    }
}