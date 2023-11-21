using UnityEngine;

namespace Aid.Animator
{
    public class AnimatorTimerParamBehaviour : StateMachineBehaviour
    {
        [SerializeField] private string paramName;

        private int ParamHash = -1;

        public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (ParamHash == -1)
            {
                ParamHash = UnityEngine.Animator.StringToHash(paramName);
            }

            animator.SetFloat(ParamHash, 0);
        }

        public override void OnStateUpdate(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat(ParamHash, animator.GetFloat(ParamHash) + Time.deltaTime);
        }
    }
}