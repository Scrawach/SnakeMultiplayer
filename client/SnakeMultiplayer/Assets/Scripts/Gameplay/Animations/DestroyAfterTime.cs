using UnityEngine;

namespace Gameplay.Animations
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float _timeInSeconds;

        private void Awake() => 
            Destroy(gameObject, _timeInSeconds);
    }
}