using UnityEngine;

namespace EcsDotsAnimations.Controllers
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        static readonly int Speed = Animator.StringToHash("Speed");

        void OnValidate()
        {
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
        }

        public void MoveAnimation(float velocity)
        {
            _animator.SetFloat(Speed, velocity);
        }
    }
}