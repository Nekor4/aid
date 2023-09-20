using UnityEngine;
using UnityEngine.Animations;

namespace TapNice.Scripts.GameCore.Animations
{
    public class AnimatorRandomIntParam : StateMachineBehaviour
    {
        [SerializeField] private int minValue, maxValue;
        [SerializeField] private string paramName;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            animator.SetInteger(paramName, Random.Range(minValue, maxValue));
        }
    }
}