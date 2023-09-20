namespace Aid.Animator
{
    using UnityEngine;

    public class RootMotionController : MonoBehaviour
    {
        private UnityEngine.Animator animator;
        public Vector3 Velocity => animator.velocity;

        private void Awake()
        {
            animator = GetComponent<UnityEngine.Animator>();
        }

        private void OnAnimatorMove()
        {
            transform.localPosition = Vector3.zero;
        }
    }
}