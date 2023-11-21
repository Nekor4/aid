using UnityEngine;
using UnityEngine.Animations;

namespace Aid.Animator
{
    public class AnimatorRandomIntParam : StateMachineBehaviour
    {
        [SerializeField] private int minValue, maxValue;
        [SerializeField] private string paramName;

        public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            animator.SetInteger(paramName, UnityEngine.Random.Range(minValue, maxValue));
        }
    }
}