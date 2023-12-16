using UnityEngine;

namespace EcsDotsAnimations.Controllers
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] Transform _transform;
        
        static readonly int Speed = Animator.StringToHash("Speed");

        public Transform Transform => _transform;

        void OnValidate()
        {
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        public void MoveAnimation(float velocity)
        {
            _animator.SetFloat(Speed, velocity);
        }
    }
}