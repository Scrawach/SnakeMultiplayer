using UnityEngine;

namespace Gameplay.Animations
{
    public class SnakeHeadAnimator : MonoBehaviour
    {
        private static readonly int Eat = Animator.StringToHash("Eat");
        
        [SerializeField] private Animator _animator;

        public void PlayEat() => 
            _animator.SetTrigger(Eat);
    }
}